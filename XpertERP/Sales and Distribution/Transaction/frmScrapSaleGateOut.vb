Imports common
Imports System.Data.SqlClient

Public Class FrmScrapSaleGateOut
    Inherits FrmMainTranScreen
    Public isLoadData As Boolean = False
    Dim obj As clsScrapSaleGateOut = Nothing

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData()
    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(fndShipmentNo.Value) <= 0 Then
                Throw New Exception("Shipment No. Can't left blank")
            End If
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") <> CompairStringResult.Equal Then
                If clsCommon.myLen(txtVehicleNo.Text) <= 0 Then
                    Throw New Exception("Gate Out Not Allow because Vehicle No. is not Available.")
                End If
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub SaveData()
        Try
            Dim trans As SqlTransaction = Nothing
            obj = New clsScrapSaleGateOut
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            obj.Document_No = clsCommon.myCstr(fndDocNo.Value)
            obj.Gate_Out_Date = clsCommon.GetPrintDate(txtGateOutDate.Value, "dd/MMM/yyyy")
            obj.Shipment_No = clsCommon.myCstr(fndShipmentNo.Value)
            obj.Shipment_Date = clsCommon.GetPrintDate(txtShipmentDate.Text, "dd/MMM/yyyy")
            obj.Vehicle_Mannual_No = clsCommon.myCstr(txtVehicleNo.Text)
            obj.From_Location = clsCommon.myCstr(txtFromLoc.Text)
            obj.To_Location = clsCommon.myCstr(txtToLoc.Text)
            obj.Customer_Code = clsCommon.myCstr(txtCustomerCode.Text)
            obj.Transport_Id = clsCommon.myCstr(txtTransportId.Text)
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyy")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
            End If

            If clsScrapSaleGateOut.saveData(obj, trans) Then
                trans.Commit()
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully")
                End If


                loadGateoutData(obj.Document_No, NavigatorType.Current)
                btnSave.Text = "Update"
                fndDocNo.MyReadOnly = True
                btnDelete.Enabled = True

                Exit Sub
            End If
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Update"
            btnDelete.Enabled = False

            fndDocNo.MyReadOnly = False
            trans.Rollback()

        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Sub loadGateoutData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsScrapSaleGateOut.getGateOutData(str, navtype)
        If obj IsNot Nothing Then
            Reset()
            isLoadData = True
            fndDocNo.Value = obj.Document_No
            txtGateOutDate.Text = obj.Gate_Out_Date
            fndShipmentNo.Value = obj.Shipment_No
            txtShipmentDate.Text = obj.Shipment_Date
            txtVehicleId.Text = obj.Vehicle_Id
            txtVehicleNo.Text = obj.Vehicle_Mannual_No
            txtFromLoc.Text = obj.From_Location
            lblFromLoc.Text = obj.From_Location_Desc
            txtToLoc.Text = obj.To_Location
            lblToLoc.Text = obj.To_Location_Desc
            txtCustomerCode.Text = obj.Customer_Code
            txtCustomerName.Text = obj.Customer_Name
            txtTransportId.Text = obj.Transport_Id
            lblTransportName.Text = obj.Transporter_Name
        End If
        isLoadData = False
        btnSave.Text = "Update"

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If myMessages.deleteConfirm() Then
            deleteData()
        End If
    End Sub


    Sub deleteData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then

                If clsScrapSaleGateOut.deleteData(fndDocNo.Value, trans) Then
                    myMessages.delete()
                    trans.Commit()
                    Reset()
                Else
                    clsCommon.MyMessageBoxShow("Can't delete the record")
                    trans.Rollback()
                End If
            Else

                clsCommon.MyMessageBoxShow("Please Select a document to delete")
                trans.Rollback()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
    End Sub

    Sub Reset()
        fndDocNo.Value = ""
        txtShipmentDate.Text = ""
        txtGateOutDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtToLoc.Text = ""
        txtFromLoc.Text = ""
        txtVehicleId.Text = ""
        txtVehicleNo.Text = ""
        fndShipmentNo.Value = Nothing
        txtShipmentDate.Text = ""
        lblFromLoc.Text = ""
        lblToLoc.Text = ""
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtTransportId.Text = ""
        lblTransportName.Text = ""
        btnSave.Text = "Save"
    End Sub

    Private Sub FrmScrapSaleGateOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub

    Private Sub fndDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndDocNo._MYNavigator
        loadGateoutData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        fndDocNo.Value = clsScrapSaleGateOut.getGateOutFinder("", fndDocNo.Value, isButtonClicked)
        loadGateoutData(fndDocNo.Value, NavigatorType.Current)
    End Sub

    Private Sub fndShipmentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndShipmentNo._MYValidating
        fndShipmentNo.Value = clsScrapSaleGateOut.getShipmentFinder("", fndShipmentNo.Value, isButtonClicked)
        loadShipmentData(fndShipmentNo.Value, NavigatorType.Current)
    End Sub
    Sub loadShipmentData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsScrapSaleGateOut.getShipmentData(str, navtype)
        If obj IsNot Nothing Then
            Reset()
            isLoadData = True
            fndShipmentNo.Value = obj.Shipment_No
            txtShipmentDate.Text = obj.Shipment_Date
            txtVehicleId.Text = obj.Vehicle_Id
            txtVehicleNo.Text = obj.Vehicle_Mannual_No
            txtFromLoc.Text = obj.From_Location
            lblFromLoc.Text = obj.From_Location_Desc
            txtToLoc.Text = obj.To_Location
            lblToLoc.Text = obj.To_Location_Desc
            txtCustomerCode.Text = obj.Customer_Code
            txtCustomerName.Text = obj.Customer_Name
            txtTransportId.Text = obj.Transport_Id
            lblTransportName.Text = obj.Transporter_Name
        End If
        isLoadData = False
        btnSave.Text = "Save"
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
End Class
