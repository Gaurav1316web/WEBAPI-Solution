Imports common
Imports System.Data.SqlClient
Public Class FrmTransferGateOut
    Inherits FrmMainTranScreen
    Public isLoadData As Boolean = False
    Dim obj As clsTransferGateOut = Nothing

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmTransferGateOut)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData()

    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(fndTransferNo.Value) <= 0 Then
                Throw New Exception("Transfer No. Can't left blank")
            End If
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") <> CompairStringResult.Equal Then
                If clsCommon.myLen(txtVehicleNo.Text) <= 0 Then
                    Throw New Exception("Gate Out Not Allow because Vehicle No. is not Available.")
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Sub SaveData()
        Try
            Dim trans As SqlTransaction = Nothing
            obj = New clsTransferGateOut
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            obj.Document_No = clsCommon.myCstr(fndDocNo.Value)
            obj.Gate_Out_Date = clsCommon.GetPrintDate(txtGateOutDate.Value, "dd/MMM/yyyy")
            obj.Transfer_No = clsCommon.myCstr(fndTransferNo.Value)
            obj.Transfer_Date = clsCommon.GetPrintDate(txtTransferDate.Text, "dd/MMM/yyyy")
            obj.Vehicle_Mannual_No = clsCommon.myCstr(txtVehicleNo.Text)
            obj.From_Location = clsCommon.myCstr(txtFromLoc.Text)
            obj.To_Location = clsCommon.myCstr(txtToLoc.Text)
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyy")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
            End If

            If clsTransferGateOut.saveData(obj, trans) Then
                trans.Commit()
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If


                loadGateoutData(obj.Document_No, NavigatorType.Current)
                btnSave.Text = "Update"
                fndDocNo.MyReadOnly = True
                btnDelete.Enabled = True

                Exit Sub
            End If
            clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
            btnSave.Text = "Update"
            btnDelete.Enabled = False

            fndDocNo.MyReadOnly = False
            trans.Rollback()

        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub


    Sub Reset()
        fndDocNo.Value = ""
        txtTransferDate.Text = ""
        txtGateOutDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtToLoc.Text = ""
        txtFromLoc.Text = ""
        txtVehicleId.Text = ""
        txtVehicleNo.Text = ""
        fndTransferNo.Value = Nothing
        lblFromLoc.Text = ""
        lblToLoc.Text = ""
        btnSave.Text = "Save"
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmTransferGateOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub

    Private Sub fndTransferNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTransferNo._MYValidating
        fndTransferNo.Value = clsTransferGateOut.getTransferFinder("", fndTransferNo.Value, isButtonClicked)
        loadTransferData(fndTransferNo.Value, NavigatorType.Current)
    End Sub
    Sub loadTransferData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsTransferGateOut.getTransferData(str, navtype)
        If obj IsNot Nothing Then
            Reset()
            isLoadData = True
            fndTransferNo.Value = obj.Transfer_No
            txtTransferDate.Text = obj.Transfer_Date
            txtVehicleId.Text = obj.Vehicle_Id
            txtVehicleNo.Text = obj.Vehicle_Mannual_No
            txtFromLoc.Text = obj.From_Location
            lblFromLoc.Text = obj.From_Location_Desc
            txtToLoc.Text = obj.To_Location
            lblToLoc.Text = obj.To_Location_Desc
        End If
        isLoadData = False
        btnSave.Text = "Save"
    End Sub

    Sub loadGateoutData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsTransferGateOut.getGateOutData(str, navtype)
        If obj IsNot Nothing Then
            Reset()
            isLoadData = True
            fndDocNo.Value = obj.Document_No
            txtGateOutDate.Text = obj.Gate_Out_Date
            fndTransferNo.Value = obj.Transfer_No
            txtTransferDate.Text = obj.Transfer_Date
            txtVehicleId.Text = obj.Vehicle_Id
            txtVehicleNo.Text = obj.Vehicle_Mannual_No
            txtFromLoc.Text = obj.From_Location
            lblFromLoc.Text = obj.From_Location_Desc
            txtToLoc.Text = obj.To_Location
            lblToLoc.Text = obj.To_Location_Desc
        End If
        isLoadData = False
        btnSave.Text = "Update"
    End Sub


    Private Sub fndDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        fndDocNo.Value = clsTransferGateOut.getGateOutFinder("", fndDocNo.Value, isButtonClicked)
        loadGateoutData(fndDocNo.Value, NavigatorType.Current)
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

                If clsTransferGateOut.deleteData(fndDocNo.Value, trans) Then
                    myMessages.delete()
                    trans.Commit()
                    Reset()
                Else
                    clsCommon.MyMessageBoxShow(Me, "Can't delete the record", Me.Text)
                    trans.Rollback()
                End If
            Else

                clsCommon.MyMessageBoxShow(Me, "Please Select a document to delete", Me.Text)
                trans.Rollback()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub



    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub fndDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndDocNo._MYNavigator
        loadGateoutData(fndDocNo.Value, NavType)
    End Sub
End Class
