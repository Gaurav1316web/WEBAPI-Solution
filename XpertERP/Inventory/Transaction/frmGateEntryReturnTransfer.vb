Imports common
Imports System.Data.SqlClient
Public Class frmGateEntryReturnTransfer
    Inherits FrmMainTranScreen
    Private MainFormId As String = clsUserMgtCode.frmGateEntryReturnTransfer
    Public isLoadData As Boolean = False
    Private isNewEntry As Boolean = False
    Dim obj As clsGateEntryReturnTransfer = Nothing

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(MainFormId)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If myMessages.deleteConfirm() Then
            deleteData()
        End If
    End Sub
    Function allowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDocDate.Value, Nothing) = False Then
                txtDocDate.Focus()
                Return False
            End If
            If clsCommon.myLen(fndRefDocNo.Value) <= 0 Then
                Throw New Exception("Pls Select Transfer No for Return.")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub SaveData()
        Try
            Dim trans As SqlTransaction = Nothing
            obj = New clsGateEntryReturnTransfer

            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            obj.GE_CODE = clsCommon.myCstr(fndDocNo.Value)
            obj.GE_DATE = clsCommon.GetPrintDate(txtDocDate.Value, "dd/MMM/yyyy")
            obj.REF_DOC_No = clsCommon.myCstr(fndRefDocNo.Value)
            obj.Manual_Vehicle = clsCommon.myCstr(txtVehicleNo.Text)
            obj.Customer = clsCommon.myCstr(txtCustomerCode.Text)
            obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
            If clsGateEntryReturnTransfer.SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If


                loadData(obj.GE_CODE, NavigatorType.Current)
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
    Sub loadData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsGateEntryReturnTransfer.GetData(str, navtype)
        If obj IsNot Nothing Then
            Reset()
            isLoadData = True
            fndDocNo.MyReadOnly = True
            isNewEntry = False
            fndDocNo.Value = obj.GE_CODE
            txtDocDate.Text = obj.GE_DATE
            fndRefDocNo.Value = obj.REF_DOC_No
            If clsCommon.myLen(fndRefDocNo.Value) > 0 Then
                txtRefDocDate.Text = clsGateEntryReturnTransfer.GetRefDocDate(fndRefDocNo.Value, Nothing)
            End If
            txtVehicleNo.Text = obj.Manual_Vehicle
            txtRemarks.Text = obj.Remarks
            txtCustomerCode.Text = obj.Customer
            'txtCustomerName.Text = clsGateEntryReturnTransfer.GetRefDocDate(txtCustomerCode.Text, Nothing)
            If obj.Posted = 1 Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
            End If
        End If
        isLoadData = False
        btnSave.Text = "Update"

    End Sub

    Sub deleteData()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then

                If clsGateEntryReturnTransfer.DeleteData(fndDocNo.Value) Then
                    myMessages.delete()
                    Reset()
                Else
                    clsCommon.MyMessageBoxShow(Me, "Can't delete the record", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Select a document to delete", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub Reset()
        isNewEntry = True
        fndDocNo.Value = ""
        txtRefDocDate.Text = ""
        txtDocDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtVehicleNo.Text = ""
        fndRefDocNo.Value = Nothing
        txtRefDocDate.Text = ""
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtRemarks.Text = ""
        btnSave.Text = "Save"
        fndDocNo.MyReadOnly = False
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
    End Sub

    Private Sub FrmProductDispatchGateOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub

    Private Sub fndDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndDocNo._MYNavigator
        loadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        fndDocNo.Value = clsGateEntryReturnTransfer.getFinder("", fndDocNo.Value, isButtonClicked)
        loadData(fndDocNo.Value, NavigatorType.Current)
    End Sub

    Private Sub fndDispatchNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRefDocNo._MYValidating
        fndRefDocNo.Value = clsGateEntryReturnTransfer.getRefDocFinder("Transfer_Type='O' and Status=1", fndRefDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndRefDocNo.Value) > 0 Then
            txtRefDocDate.Text = clsGateEntryReturnTransfer.GetRefDocDate(fndRefDocNo.Value, Nothing)
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        If myMessages.postConfirm() Then
            PostData()
        End If
    End Sub
    Sub PostData()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsGateEntryReturnTransfer.PostData(fndDocNo.Value) Then
                    myMessages.post()
                    loadData(fndDocNo.Value, NavigatorType.Current)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Can't Post the record", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Select a document to Post", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
