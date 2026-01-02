Imports System.Reflection
Imports common
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class FrmEwaybill
    Inherits FrmMainTranScreen
#Region "Variables"
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub FrmEwaybill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        CreateTable()
        LoadEwaybillType()
        LoadvechicleReasonType()
        LoadExtendValidity()
        LoadConsignmentType()
        Reset()
    End Sub
    Sub LoadEwaybillType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "-- Select Eway Bill Types --"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Eway Bill Details"
        dr("Name") = "Eway Bill Details"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Update Transpoter"
        dr("Name") = "Update Transpoter"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Update PART-B"
        dr("Name") = "Update PART-B/Vehicle Number"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Cancel Eway Bill"
        dr("Name") = "Cancel Eway Bill"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Eway Bills By Date"
        dr("Name") = "Eway Bills By Date"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Extend Validity of Eway Bill"
        dr("Name") = "Extend Validity of Eway Bill"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Error List"
        dr("Name") = "Error List"
        dt.Rows.Add(dr)
        cmbEwaybillType.ValueMember = "Code"
        cmbEwaybillType.DisplayMember = "Name"
        cmbEwaybillType.DataSource = dt
    End Sub
    Sub LoadvechicleReasonType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "-- Select Reason Type --"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Due to Breakdown"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "Due to Transshipment"
        dt.Rows.Add(dr)
        'dr = dt.NewRow()
        'dr("Code") = "4"
        'dr("Name") = "First Time "
        'dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "3"
        dr("Name") = "Others"
        dt.Rows.Add(dr)
        cmbReasonForUpdateVehicle.ValueMember = "Code"
        cmbReasonForUpdateVehicle.DisplayMember = "Name"
        cmbReasonForUpdateVehicle.DataSource = dt
    End Sub
    Sub LoadExtendValidity()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "-- Select Extend Validity Reason --"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Natural Calamity"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "Law and Order Situation"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "4"
        dr("Name") = "Transshipment"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "5"
        dr("Name") = "Accident"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "99"
        dr("Name") = "Others"
        dt.Rows.Add(dr)
        cmbExtendValidityReason.ValueMember = "Code"
        cmbExtendValidityReason.DisplayMember = "Name"
        cmbExtendValidityReason.DataSource = dt
    End Sub
    Sub LoadConsignmentType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "-- Select Consignment Status --"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "inMovement"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "inTransit"
        dt.Rows.Add(dr)
        cmbConsignmentStatus.ValueMember = "Code"
        cmbConsignmentStatus.DisplayMember = "Name"
        cmbConsignmentStatus.DataSource = dt
    End Sub
    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Private Sub Reset()
        cmbEwaybillType.SelectedValue = ""
        txtewbno.Enabled = False
        txtewbno.Value = ""
        txtTransID.Value = ""
        txtVehicleNo.Value = ""
        cmbExtendValidityReason.SelectedValue = ""
        cmbReasonForUpdateVehicle.SelectedValue = ""
        cmbConsignmentStatus.SelectedValue = ""
        txtRemaningDistance.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDate.Enabled = False
        txtLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim objResult As Object = Nothing
            If clsCommon.CompairString(cmbEwaybillType.SelectedValue, "") = CompairStringResult.Equal Then
                Throw New Exception("Please Select Eway Bill Type")
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Eway Bill Details") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtewbno.Value) > 0 Then
                    Try
                        Dim isewbno As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(EWBNO) from TSPL_EWAY_BILL_REPORT_DETAIL where EWBNO='" & txtewbno.Value & "' "))
                        If isewbno = 0 Then
                            GetEwayBill(txtewbno.Value, True)
                        Else
                            LoadEWBDetail(txtewbno.Value)
                        End If
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try
                End If
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Update Transpoter") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtewbno.Value) > 0 AndAlso clsCommon.myLen(txtTransID.Value) > 0 Then
                    'Dim BillToLocaiton As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bill_To_Location from tspl_sd_sale_invoice_head where EWayBillNo='" & clsCommon.myCstr(txtewbno.value) & "'", Nothing))
                    Dim TransID As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GSTFinalNo,*  from TSPL_VENDOR_MASTER where Vendor_Code='" & txtTransID.Value & "' and Vendor_Group_Code='TPT'", Nothing))
                    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from tspl_sd_sale_invoice_head where EWayBillNo='" & clsCommon.myCstr(txtewbno.Value) & "'", Nothing))
                    If clsCommon.myLen(TransID) > 0 Then
                        objResult = ClsEInvoiceOFAPIs.EWayBill_Update_Transporter(objCommonVar.CurrentCompanyCode, txtewbno.Value, TransID, txtLocation.Value, Nothing)
                        If objResult Is Nothing Then
                            Throw New Exception("Update Transpoter failed!")
                        Else
                            Dim ewayBillNo As String = objResult.SelectToken("data.ewayBillNo").ToString
                            Dim transporterId As String = objResult.SelectToken("data.transporterId").ToString
                            Dim transUpdateDate As String = objResult.SelectToken("data.transUpdateDate").ToString
                            Dim strTransCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER where GSTFinalNo='" & transporterId & "'"))
                            Dim strUpdateQry As String = "update tspl_sd_sale_invoice_head set Transport_Code='" & clsCommon.myCstr(strTransCode) & "',transUpdateDate='" & clsCommon.GetPrintDate(clsCommon.myCDate(transUpdateDate), "dd/MMM/yyyy hh:mm tt") & "' where EWayBillNo='" & clsCommon.myCstr(ewayBillNo) & "'"
                            clsDBFuncationality.ExecuteNonQuery(strUpdateQry)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "TSPL_SD_SALE_INVOICE_DETAIL", "Document_Code", Nothing)
                            clsCommon.MyMessageBoxShow(Me, "Update Transpoter Successfully." & Environment.NewLine & "transporterId :" & strTransCode & Environment.NewLine & "Update Date :" & transUpdateDate, Me.Text)
                        End If
                    Else
                        Throw New Exception("Gstno not map with Transport [" & txtTransID.Value & "]")
                    End If
                Else
                    Throw New Exception("Please Enter ewb no/select Transpoter")
                End If
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Update PART-B") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtewbno.Value) > 0 AndAlso clsCommon.CompairString(cmbReasonForUpdateVehicle.SelectedValue, "") <> CompairStringResult.Equal AndAlso clsCommon.myLen(txtVehicleNo.Value) > 0 Then
                    Dim isewbno As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(EWBNO) from TSPL_EWAY_BILL_REPORT_DETAIL where EWBNO='" & txtewbno.Value & "' "))
                    Dim vehicleNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicleNo.Value & "' "))
                    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from tspl_sd_sale_invoice_head where EWayBillNo='" & clsCommon.myCstr(txtewbno.Value) & "'", Nothing))
                    If isewbno = 0 Then
                        GetEwayBill(txtewbno.Value, False)
                    Else
                        Dim obj As New clsEwayBillReportHead()
                        obj = clsEwayBillReportHead.GetData(txtewbno.Value)
                        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.ewbno) > 0 Then
                            Dim ObjVehicle As New clsEwayBillUpdatePartB_Vehicle()
                            ObjVehicle.ewbNo = obj.ewbno
                            ObjVehicle.vehicleNo = vehicleNo
                            ObjVehicle.fromPlace = obj.fromPlace
                            ObjVehicle.fromState = obj.fromStateCode
                            ObjVehicle.reasonCode = clsCommon.myCstr(cmbReasonForUpdateVehicle.SelectedValue)
                            ObjVehicle.reasonRem = cmbReasonForUpdateVehicle.Text
                            If obj.VehiclListDetails.Count > 0 Then
                                ObjVehicle.transDocNo = obj.VehiclListDetails(0).transDocNo
                                ObjVehicle.transDocDate = clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MM/yyyy")) 'obj.VehiclListDetails(0).transDocDate
                                ObjVehicle.transMode = obj.VehiclListDetails(0).transMode
                            End If
                            objResult = ClsEInvoiceOFAPIs.EWayBill_Update_PartB(objCommonVar.CurrentCompanyCode, ObjVehicle, txtLocation.Value, Nothing)
                            If objResult Is Nothing Then
                                Throw New Exception("Update Vehicle no failed!")
                            Else
                                Dim josnReult As String = objResult.SelectToken("data").ToString
                                Dim vehUpdDate As String = objResult.SelectToken("data.vehUpdDate").ToString
                                Dim validUpto As String = objResult.SelectToken("data.validUpto").ToString
                                Dim strUpdateQry As String = "update tspl_sd_sale_invoice_head set vehUpdDate='" & clsCommon.GetPrintDate(clsCommon.myCDate(vehUpdDate), "dd/MMM/yyyy hh:mm tt") & "',EWayBillValidDate='" & clsCommon.GetPrintDate(clsCommon.myCDate(validUpto), "dd/MMM/yyyy hh:mm tt") & "' where EWayBillNo='" & clsCommon.myCstr(txtewbno.Value) & "'"
                                clsDBFuncationality.ExecuteNonQuery(strUpdateQry)
                                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "TSPL_SD_SALE_INVOICE_DETAIL", "Document_Code", Nothing)
                                clsCommon.MyMessageBoxShow(Me, "Update Prat-B/Vehicle number Successfully." & Environment.NewLine & "Vehicle Update Date :" & vehUpdDate & " " & Environment.NewLine & "validUpto :" & validUpto, Me.Text)
                            End If
                        End If
                    End If
                Else
                    Throw New Exception("Please Enter ewb no/Select Update Vehicle Reason.")
                End If
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Cancel Eway Bill") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtewbno.Value) > 0 Then
                    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from tspl_sd_sale_invoice_head where EWayBillNo='" & clsCommon.myCstr(txtewbno.Value) & "'", Nothing))
                    If clsCommon.myLen(txtLocation.Value) > 0 Then
                        objResult = ClsEInvoiceOFAPIs.CancelEWayBill(objCommonVar.CurrentCompanyCode, txtewbno.Value, "Order Cancelled", txtLocation.Value, Nothing)
                        If objResult Is Nothing Then
                            Throw New Exception("e-way bill cancellation failed!")
                        Else
                            Dim CancelDate As String = objResult.SelectToken("data.cancelDate").ToString
                            Dim strUpdateQry As String = "update tspl_sd_sale_invoice_head set Ewb_cancelDate='" & clsCommon.GetPrintDate(clsCommon.myCDate(CancelDate), "dd/MMM/yyyy hh:mm tt") & "' where EWayBillNo='" & clsCommon.myCstr(txtewbno.Value) & "'"
                            clsDBFuncationality.ExecuteNonQuery(strUpdateQry)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "TSPL_SD_SALE_INVOICE_DETAIL", "Document_Code", Nothing)
                            clsCommon.MyMessageBoxShow(Me, "Cancelled Successfully.", Me.Text)
                        End If
                    End If
                End If
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Eway Bills By Date") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    objResult = ClsEInvoiceOFAPIs.GetEwayBillByDate(objCommonVar.CurrentCompanyCode, txtDate.Value, txtLocation.Value, Nothing)
                    If objResult Is Nothing Then
                        Throw New Exception("Get Ewb Bills failed!")
                    Else
                        Dim ewayBillNo As String = objResult.SelectToken("data").ToString
                        'Dim transporterId As String = objResult.SelectToken("data.transporterId").ToString
                        'Dim transUpdateDate As String = objResult.SelectToken("data.transUpdateDate").ToString
                        'Dim strTransCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER where GSTFinalNo='" & transporterId & "'"))
                        clsCommon.MyMessageBoxShow(Me, ewayBillNo, Me.Text)
                    End If
                Else
                    Throw New Exception("Please Select Locaiton!")
                End If
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Extend Validity of Eway Bill") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtewbno.Value) > 0 AndAlso clsCommon.CompairString(cmbExtendValidityReason.SelectedValue, "") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbConsignmentStatus.SelectedValue, "") <> CompairStringResult.Equal AndAlso clsCommon.myCdbl(txtRemaningDistance.Text) > 0 Then
                    Dim isewbno As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(EWBNO) from TSPL_EWAY_BILL_REPORT_DETAIL where EWBNO='" & txtewbno.Value & "' "))
                    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from tspl_sd_sale_invoice_head where EWayBillNo='" & clsCommon.myCstr(txtewbno.Value) & "'", Nothing))
                    If isewbno = 0 Then
                        GetEwayBill(txtewbno.Value, False)
                    Else
                        Dim obj As New clsEwayBillReportHead()
                        obj = clsEwayBillReportHead.GetData(txtewbno.Value)
                        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.ewbno) > 0 Then
                            Dim objExtend As New clsextendvalidity()
                            Dim EwbExtendTimeValid As Int64 = 0
                            EwbExtendTimeValid = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select  isnull (DATEDIFF(hour,EWayBillValidDate,GETDATE()),0) as validTill from tspl_sd_sale_invoice_head where  document_code = '" & strDocNo & "'"))
                            If EwbExtendTimeValid >= 8 OrElse EwbExtendTimeValid < -8 Then
                                Throw New Exception("The validity of EWB can be extended between 8 hours before expiry time and 8 hours after expiry time.")
                            End If
                            objExtend.ewbNo = clsCommon.myCdbl(obj.ewbno)
                            objExtend.vehicleNo = clsCommon.myCstr(obj.VehiclListDetails(0).vehicleNo)
                            objExtend.fromPlace = clsCommon.myCstr(obj.VehiclListDetails(0).fromPlace)
                            objExtend.fromState = clsCommon.myCdbl(obj.VehiclListDetails(0).fromState)
                            objExtend.remainingDistance = clsCommon.myCdbl(txtRemaningDistance.Text)
                            objExtend.transDocNo = clsCommon.myCstr(obj.VehiclListDetails(0).transDocNo)
                            objExtend.transDocDate = clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MM/yyyy"))
                            objExtend.transMode = clsCommon.myCstr(obj.VehiclListDetails(0).transMode)
                            If clsCommon.myCdbl(obj.VehiclListDetails(0).transMode) >= 1 AndAlso clsCommon.myCdbl(obj.VehiclListDetails(0).transMode) <= 4 Then
                                objExtend.transitType = ""
                                objExtend.addressLine1 = ""
                                objExtend.addressLine2 = ""
                                objExtend.addressLine3 = ""
                                If clsCommon.CompairString(cmbConsignmentStatus.SelectedValue, "M") <> CompairStringResult.Equal Then
                                    Throw New Exception("Consignmgnet status should be inMovement")
                                End If
                            ElseIf clsCommon.myCdbl(obj.VehiclListDetails(0).transMode) = 5 Then
                                objExtend.transitType = clsCommon.myCstr(obj.transactionType)
                                objExtend.addressLine1 = clsCommon.myCstr(obj.toAddr1)
                                objExtend.addressLine2 = clsCommon.myCstr(obj.toAddr2)
                                objExtend.addressLine3 = clsCommon.myCstr(obj.toAddr2)
                                If clsCommon.CompairString(cmbConsignmentStatus.SelectedValue, "T") <> CompairStringResult.Equal Then
                                    Throw New Exception("Consignmgnet status should be inTransit")
                                End If
                            End If
                            objExtend.extnRsnCode = clsCommon.myCdbl(cmbExtendValidityReason.SelectedValue)
                            objExtend.extnRemarks = clsCommon.myCstr(cmbExtendValidityReason.Text)
                            objExtend.fromPincode = clsCommon.myCdbl(obj.fromPincode)
                            objExtend.consignmentStatus = clsCommon.myCstr(cmbConsignmentStatus.SelectedValue)
                            objResult = ClsEInvoiceOFAPIs.EWayBill_ExtendValidity(objCommonVar.CurrentCompanyCode, objExtend, txtLocation.Value, Nothing)
                            If objResult Is Nothing Then
                                Throw New Exception("Extend Validity Failed!")
                            Else
                                Dim ewayBillNo As String = objResult.SelectToken("data.ewayBillNo").ToString
                                Dim updatedDate As String = objResult.SelectToken("data.updatedDate").ToString
                                Dim validUpto As String = objResult.SelectToken("data.validUpto").ToString
                                Dim strUpdateQry As String = "update tspl_sd_sale_invoice_head set ExtendValidityUpdate='" & clsCommon.GetPrintDate(clsCommon.myCDate(updatedDate), "dd/MMM/yyyy hh:mm tt") & "',EWayBillValidDate='" & clsCommon.GetPrintDate(clsCommon.myCDate(validUpto), "dd/MMM/yyyy hh:mm tt") & "' where EWayBillNo='" & clsCommon.myCstr(ewayBillNo) & "'"
                                clsDBFuncationality.ExecuteNonQuery(strUpdateQry)
                                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "TSPL_SD_SALE_INVOICE_DETAIL", "Document_Code", Nothing)
                                clsCommon.MyMessageBoxShow(Me, "Extend Validity Successfully." & Environment.NewLine & objResult.SelectToken("data").ToString, Me.Text)
                            End If
                        End If
                    End If
                Else
                    Throw New Exception("Please Enter ewb no/Select Update Vehicle Reason/Select Consignmnet Status/enter Remaining Distance")
                End If
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Error List") = CompairStringResult.Equal Then
                Throw New Exception("This API endpoint is not currently available.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub GetEwayBill(ByVal strewbno As String, ByVal isGetewbno As Boolean)
        Try
            Dim objResult As Object = ClsEInvoiceOFAPIs.GetEwayBillDetail(objCommonVar.CurrentCompanyCode, txtewbno.Value, txtLocation.Value, Nothing)
            If objResult IsNot Nothing Then
                Dim Ewb_Json As String = objResult.SelectToken("data").ToString
                clsEwayBillReportHead.SaveData(Ewb_Json)
                If isGetewbno Then
                    LoadEWBDetail(strewbno)
                End If
            Else
                Throw New Exception("Something went worng!")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub LoadEWBDetail(ByVal strewbno As String)
        Try
            'If clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Eway Bill Details") = CompairStringResult.Equal Then
            '    Dim obj As New clsEwayBillReportHead()
            '    obj = clsEwayBillReportHead.GetData(strewbno)
            '    If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ewbno) > 0) Then
            '    End If
            'ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Update Transpoter") = CompairStringResult.Equal Then
            'End If
            Dim Qry As String = " select cast(TSPL_SD_SALE_INVOICE_HEAD.EWayBill_QR_Code as image) as EWayBill_QR_Code,TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo,TSPL_SD_SALE_INVOICE_HEAD.ewayBillDate,TSPL_COMPANY_MASTER.GSTReg_No +' ' + TSPL_COMPANY_MASTER.Comp_Name as Generated_By,TSPL_SD_SALE_INVOICE_HEAD.EWayBillValidDate,
TSPL_SD_SALE_INVOICE_HEAD.EWayBillRemarks,TSPL_SD_SALE_INVOICE_HEAD.Freight_Distance,TSPL_SD_SALE_INVOICE_HEAD.IRN_No,TSPL_SD_SALE_INVOICE_HEAD.Ack_No,TSPL_SD_SALE_INVOICE_HEAD.Ack_Date,TSPL_CUSTOMER_MASTER.GSTNO+' '+ TSPL_CUSTOMER_MASTER.Customer_Name+' '+TSPL_CUSTOMER_MASTER.City_Code as GSTIN_Of_Recipient,TSPL_COMPANY_MASTER.City_Code+' - '+TSPL_COMPANY_MASTER.State+' - '+TSPL_COMPANY_MASTER.Pincode as Place_Of_Dispatch, TSPL_CUSTOMER_MASTER.City_Code+' '+TSPL_CUSTOMER_MASTER.State +' '+CONVERT(varchar(10),isNull(TSPL_CUSTOMER_MASTER.PIN_Code,'')) as Place_Of_delivery,
TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,'Regular' as Transaction_Type,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,TSPL_ITEM_MASTER.HSN_Code +' '+TSPL_ITEM_MASTER.Short_Description  as HsnCode,
--convert(VARCHAR(10), case when(select COUNT(*) from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code)-1>0 then (select COUNT(*) from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code)-1  else '' end) as countofItem,
'Outward - Supply' as ResonofTransport,TSPL_SD_SALE_INVOICE_HEAD.Trans_Type,TSPL_SD_SALE_INVOICE_HEAD.Transporter_Name,'Road' As Mode_of_Trans,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location
from TSPL_SD_SALE_INVOICE_HEAD
left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE= TSPL_SD_SALE_INVOICE_HEAD.Document_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
where TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo='" & strewbno & "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rpte-waybill", "E-WayBill", clsCommon.GetPrintDate(txtDate.Value), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                frmCRV = Nothing
            Else
                Throw New Exception("No Data Found ")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub cmbEwaybillType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbEwaybillType.SelectedIndexChanged
        Try
            If clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Eway Bill Details") = CompairStringResult.Equal Then
                txtewbno.Enabled = True
                txtTransID.Enabled = False
                cmbReasonForUpdateVehicle.Enabled = False
                cmbExtendValidityReason.Enabled = False
                txtVehicleNo.Enabled = False
                txtRemaningDistance.Enabled = False
                cmbConsignmentStatus.Enabled = False
                txtDate.Enabled = False
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Update Transpoter") = CompairStringResult.Equal Then
                txtewbno.Enabled = True
                txtTransID.Enabled = True
                cmbReasonForUpdateVehicle.Enabled = False
                cmbExtendValidityReason.Enabled = False
                txtVehicleNo.Enabled = False
                txtRemaningDistance.Enabled = False
                cmbConsignmentStatus.Enabled = False
                txtDate.Enabled = False
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Update PART-B") = CompairStringResult.Equal Then
                txtewbno.Enabled = True
                txtTransID.Enabled = False
                cmbReasonForUpdateVehicle.Enabled = True
                cmbExtendValidityReason.Enabled = False
                txtVehicleNo.Enabled = True
                txtRemaningDistance.Enabled = False
                cmbConsignmentStatus.Enabled = False
                txtDate.Enabled = False
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Cancel Eway Bill") = CompairStringResult.Equal Then
                txtewbno.Enabled = True
                txtTransID.Enabled = False
                cmbReasonForUpdateVehicle.Enabled = False
                cmbExtendValidityReason.Enabled = False
                txtVehicleNo.Enabled = False
                txtRemaningDistance.Enabled = False
                cmbConsignmentStatus.Enabled = False
                txtDate.Enabled = False
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Eway Bills By Date") = CompairStringResult.Equal Then
                txtewbno.Enabled = False
                txtTransID.Enabled = False
                cmbReasonForUpdateVehicle.Enabled = False
                cmbExtendValidityReason.Enabled = False
                txtVehicleNo.Enabled = False
                txtRemaningDistance.Enabled = False
                cmbConsignmentStatus.Enabled = False
                txtDate.Enabled = True
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Extend Validity of Eway Bill") = CompairStringResult.Equal Then
                txtewbno.Enabled = True
                txtTransID.Enabled = False
                cmbReasonForUpdateVehicle.Enabled = False
                cmbExtendValidityReason.Enabled = True
                txtVehicleNo.Enabled = False
                txtRemaningDistance.Enabled = True
                cmbConsignmentStatus.Enabled = True
                txtDate.Enabled = False
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Error List") = CompairStringResult.Equal Then
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub CreateTable()
        Try
            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)()
            coll.Add("ewbNo", "VARCHAR(30) NOT NULL Primary key")
            coll.Add("ewayBillDate", "VARCHAR(30) NOT NULL")
            coll.Add("genMode", "Varchar(30) null")
            coll.Add("userGstin", "varchar(50) NULL")
            coll.Add("supplyType", "varchar(30) NULL")
            coll.Add("subSupplyType", "varchar(30) NULL")
            coll.Add("docType", "varchar(30) NULL")
            coll.Add("docNo", "varchar(30) NULL")
            coll.Add("docDate", "varchar(30) NULL")
            coll.Add("fromGstin", "varchar(30) NULL")
            coll.Add("fromTrdName", "varchar(100) NULL")
            coll.Add("fromAddr1", "varchar(100) NULL")
            coll.Add("fromAddr2", "varchar(100) NULL")
            coll.Add("fromPlace", "varchar(30) NULL")
            coll.Add("fromPincode", "varchar(30) NULL")
            coll.Add("fromStateCode", "varchar(30) NULL")
            coll.Add("toGstin", "varchar(30) NULL")
            coll.Add("toTrdName", "varchar(100) NULL")
            coll.Add("toAddr1", "varchar(100) NULL")
            coll.Add("toAddr2", "varchar(100) NULL")
            coll.Add("toPlace", "varchar(30) NULL")
            coll.Add("toPincode", "varchar(30) NULL")
            coll.Add("toStateCode", "varchar(30) NULL")
            coll.Add("totalValue", "varchar(30) NULL")
            coll.Add("totInvValue", "varchar(30) NULL")
            coll.Add("cgstValue", "varchar(30) NULL")
            coll.Add("sgstValue", "varchar(30) NULL")
            coll.Add("igstValue", "varchar(30) NULL")
            coll.Add("cessValue", "varchar(30) NULL")
            coll.Add("transporterId", "varchar(30) NULL")
            coll.Add("transporterName", "varchar(30) NULL")
            coll.Add("status", "varchar(30) NULL")
            coll.Add("actualDist", "varchar(30) NULL")
            coll.Add("noValidDays", "varchar(30) NULL")
            coll.Add("validUpto", "varchar(30) NULL")
            coll.Add("extendedTimes", "varchar(30) NULL")
            coll.Add("rejectStatus", "varchar(30) NULL")
            coll.Add("vehicleType", "varchar(30) NULL")
            coll.Add("actFromStateCode", "varchar(30) NULL")
            coll.Add("actToStateCode", "varchar(30) NULL")
            coll.Add("transactionType", "varchar(30) NULL")
            coll.Add("otherValue", "varchar(30) NULL")
            coll.Add("cessNonAdvolValue", "varchar(30) NULL")
            clsCommonFunctionality.CreateOrAlterTable("TSPL_EWAY_BILL_REPORT_DETAIL", coll)
            coll = New Dictionary(Of String, String)()
            coll.Add("ewbNo", "Varchar(30) not null References TSPL_EWAY_BILL_REPORT_DETAIL(ewbNo)")
            coll.Add("itemNo", "Varchar(30)")
            coll.Add("productId", "Varchar(30) ")
            coll.Add("productName", "Varchar(30) ")
            coll.Add("productDesc", "Varchar(30) ")
            coll.Add("hsnCode", "Varchar(30) ")
            coll.Add("quantity", "Varchar(30) ")
            coll.Add("qtyUnit", "Varchar(30) ")
            coll.Add("cgstRate", "Varchar(30) ")
            coll.Add("sgstRate", "Varchar(30) ")
            coll.Add("igstRate", "Varchar(30) ")
            coll.Add("cessRate", "Varchar(30) ")
            coll.Add("cessNonAdvol", "Varchar(30) ")
            coll.Add("taxableAmount", "Varchar(30) ")
            clsCommonFunctionality.CreateOrAlterTable("TSPL_EWAY_BILL_REPORT_ITEM_DETAIL", coll)
            coll = New Dictionary(Of String, String)()
            coll.Add("ewbNo", "Varchar(30) not null References TSPL_EWAY_BILL_REPORT_DETAIL(ewbNo)")
            coll.Add("updMode", "Varchar(30)")
            coll.Add("vehicleNo", "Varchar(30) ")
            coll.Add("fromPlace", "Varchar(30) ")
            coll.Add("fromState", "Varchar(30) ")
            coll.Add("tripshtNo", "Varchar(30) ")
            coll.Add("userGSTINTransin", "Varchar(30) ")
            coll.Add("enteredDate", "Varchar(30) ")
            coll.Add("transMode", "Varchar(30) ")
            coll.Add("transDocNo", "Varchar(30) ")
            coll.Add("transDocDate", "Varchar(30) ")
            coll.Add("groupNo", "Varchar(30) ")
            clsCommonFunctionality.CreateOrAlterTable("TSPL_EWAY_BILL_REPORT_VEHICLE_DETAIL", coll)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtTransID__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTransID._MYValidating
        Try
            Dim qry As String = "select Transport_Id,Transporter_Name from TSPL_TRANSPORT_MASTER"
            txtTransID.Value = clsCommon.ShowSelectForm("DSTransport No", qry, "Transport_Id", "", txtTransID.Value, "Transport_Id", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
            '' Dim WhrCls As String = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N'  "
            Dim WhrCls As String = " 2=2 "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("DS-ewbLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txtVehicleNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVehicleNo._MYValidating
        Try
            Dim qry As String = "Select distinct  vehicle_id ,Number,Description from TSPL_VEHICLE_MASTER"
            txtVehicleNo.Value = clsCommon.ShowSelectForm("Vehicle No", qry, "vehicle_id", "", txtVehicleNo.Value, "vehicle_id", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtewbno__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtewbno._MYValidating
        Try
            Dim strqry As String = "select EWayBillNo as ewbno,Document_Code as Invoice_no,Document_Date as Invoice_Date from TSPL_SD_SALE_INVOICE_HEAD"
            Dim Whrcls As String = " EWayBillNo is not null and convert(date,document_date,103)>='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE().AddDays(-30)) & "' and convert(date,document_date,103)<='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE()) & "'"
            txtewbno.Value = clsCommon.ShowSelectForm("ewbno", strqry, "ewbno", Whrcls, txtewbno.Value, "Document_Date desc", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
