Imports System.Data.SqlClient
Imports common

Public Class FrmCommonUpdatesForEWB
    Inherits FrmMainTranScreen

#Region "Variables"
    Public strDocNo As String = ""
    Public strCustCode As String = ""
    Public strTransId As String = ""
    Public strTransName As String = ""
    Public strVehicleNo As String = ""
    Public strScreenType As String = ""


#End Region
    Private Sub FrmCommonUpdatesForEWB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDocNo.Text = strDocNo
        txtVendorno.Text = strCustCode
        txtManualVehicle.Text = strVehicleNo
        fndTransporter.Value = strTransId
        lblTransporter.Text = strTransName
    End Sub


    Private Sub chkNoTranspoter_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkNoTranspoter.ToggleStateChanged
        If chkNoTranspoter.Checked Then
            fndTransporter.Value = ""
            fndTransporter.Enabled = False
            lblTransporter.Text = ""
        Else
            fndTransporter.Enabled = True

        End If
    End Sub

    Private Sub fndTransporter__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTransporter._MYValidating
        Try
            Dim qry As String = "select Transport_Id as [Transport Id],Transporter_Name as [Transporter Name] from TSPL_TRANSPORT_MASTER"
            fndTransporter.Value = clsCommon.ShowSelectForm("RoutMastrCodFND", qry, "Transport Id", "", fndTransporter.Value, "", isButtonClicked)
            lblTransporter.Text = clsDBFuncationality.getSingleValue(" select Transporter_Name from TSPL_TRANSPORT_MASTER where Transport_Id='" & fndTransporter.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(txtDocNo.Text) > 0 Then
                If clsCommon.myLen(txtVendorno.Text) > 0 Then
                    UpdateVehicleNo(tran)
                Else
                    Throw New Exception("Please Enter Vehicle No.")
                End If
                tran.Commit()
                clsCommon.MyMessageBoxShow(Me, "Vehicle Updated.", Me.Text)
            Else
                Throw New Exception("Please Select Document")

            End If
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Shared Function GetEWayBillNo(strDocNo As String, trans As SqlTransaction) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(EWayBillNo,'') from TSPL_SD_SALE_INVOICE_head where Document_Code='" + strDocNo + "'", trans))
    End Function
    Private Function UpdateVehicleNo(ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strInvoiceNO As String = ""
            Dim strShipmentNo As String = ""
            Dim NoTransporter As Integer = IIf(chkNoTranspoter.Checked, 1, 0)
            If clsCommon.CompairString(strScreenType, "CustomerBooking") = CompairStringResult.Equal Then
                strInvoiceNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_head  where Against_Shipment_No in ( select Document_Code from TSPL_SD_SHIPMENT_HEAD where Against_Booking_No='" & txtDocNo.Text & "'  and Customer_Code='" & txtVendorno.Text & "') ", trans))
                strShipmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select ParentDocNo from TSPL_SD_SHIPMENT_HEAD where Against_Booking_No='" & txtDocNo.Text & "'  and Customer_Code='" & txtVendorno.Text & "' ", trans))
            ElseIf clsCommon.CompairString(strScreenType, "Dispatch") = CompairStringResult.Equal OrElse clsCommon.CompairString(strScreenType, "ProductDispatch") = CompairStringResult.Equal OrElse clsCommon.CompairString(strScreenType, "DCSSale") = CompairStringResult.Equal OrElse clsCommon.CompairString(strScreenType, "APSSale") = CompairStringResult.Equal Then
                strInvoiceNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SALE_INVOICE_head  where Against_Shipment_No='" & txtDocNo.Text & "'  ", trans))
                strShipmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ParentDocNo from TSPL_SD_SHIPMENT_HEAD where Document_Code='" & txtDocNo.Text & "' ", trans))
            ElseIf clsCommon.CompairString(strScreenType, "ScrapSale") = CompairStringResult.Equal Then
                strInvoiceNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select invoice_No from TSPL_SCRAPINVOICE_HEAD where shipment_No='" & txtDocNo.Text & "'  ", trans))
                strShipmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select shipment_No from tspl_scrapsale_head where shipment_No='" & txtDocNo.Text & "' ", trans))
            End If
            Dim strEwb As String = GetEWayBillNo(strInvoiceNO, trans)
            If clsCommon.myLen(strEwb) = 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Do you want to update vehicle no?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    If clsCommon.CompairString(strScreenType, "ScrapSale") = CompairStringResult.Equal Then
                        If clsCommon.myLen(fndTransporter.Value) > 0 Then
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_SCRAPINVOICE_HEAD set VehicleNo='" & txtManualVehicle.Text & "',Transport_code='" & fndTransporter.Value & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where invoice_No='" & strInvoiceNO & "'", trans)
                            clsDBFuncationality.ExecuteNonQuery("update tspl_scrapsale_head set VehicleNo='" & txtManualVehicle.Text & "',Transport_code='" & fndTransporter.Value & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where shipment_No='" & strShipmentNo & "'", trans)
                        Else
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_SCRAPINVOICE_HEAD set VehicleNo='" & txtManualVehicle.Text & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where invoice_No='" & strInvoiceNO & "'", trans)
                            clsDBFuncationality.ExecuteNonQuery("update tspl_scrapsale_head set VehicleNo='" & txtManualVehicle.Text & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where shipment_No='" & strShipmentNo & "'", trans)
                        End If
                    Else
                        If clsCommon.myLen(fndTransporter.Value) > 0 Then
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SALE_INVOICE_HEAD set VehicleNo='" & txtManualVehicle.Text & "',Transport_Code='" & fndTransporter.Value & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where Document_Code='" & strInvoiceNO & "'", trans)
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SHIPMENT_HEAD set VehicleNo='" & txtManualVehicle.Text & "',Transport_Code='" & fndTransporter.Value & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where ParentDocNo='" & strShipmentNo & "'", trans)
                        Else
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SALE_INVOICE_HEAD set VehicleNo='" & txtManualVehicle.Text & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where Document_Code='" & strInvoiceNO & "'", trans)
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SHIPMENT_HEAD set VehicleNo='" & txtManualVehicle.Text & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where ParentDocNo='" & strShipmentNo & "'", trans)
                        End If


                        If clsCommon.CompairString(strScreenType, "CustomerBooking") = CompairStringResult.Equal Then
                            If clsCommon.myLen(fndTransporter.Value) > 0 Then
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set  Is_Manual_Vehicle='Y',Manual_VehicleNo='" & txtManualVehicle.Text & "',Transport_Id='" & fndTransporter.Value & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where Document_No='" & txtDocNo.Text & "'", trans)
                            Else
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set  Is_Manual_Vehicle='Y',Manual_VehicleNo='" & txtManualVehicle.Text & "',No_Transporter='" & clsCommon.myCstr(NoTransporter) & "' where Document_No='" & txtDocNo.Text & "'", trans)

                            End If

                        End If
                    End If

                End If

            Else
                Throw New Exception("e-way bill already exists [" & strEwb & "]")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class