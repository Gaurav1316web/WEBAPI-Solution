Imports common
Imports System.Data.SqlClient
Public Class frmGateEntryReturnPS
    Inherits FrmMainTranScreen
    Private MainFormId As String = clsUserMgtCode.frmGateEntryReturnPS
    Public isLoadData As Boolean = False
    Private isNewEntry As Boolean = False
    Dim obj As clsGateEntryReturnPS = Nothing

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If myMessages.deleteConfirm() Then
            deleteData()
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(MainFormId)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Function allowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDocDate.Value, Nothing) = False Then
                txtDocDate.Focus()
                Return False
            End If
            'If clsCommon.myLen(txtVehicleNo.Text) <= 0 Then
            '    Throw New Exception("Gate Out Not Allow because Vehicle No. is not Available.")
            'End If
           
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub SaveData()
        Try
            Dim trans As SqlTransaction = Nothing
            obj = New clsGateEntryReturnPS
           
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            obj.GE_CODE = clsCommon.myCstr(fndDocNo.Value)
            obj.GE_DATE = clsCommon.GetPrintDate(txtDocDate.Value, "dd/MMM/yyyy")
            obj.REF_DOC_No = clsCommon.myCstr(fndRefDocNo.Value)
            obj.Manual_Vehicle = clsCommon.myCstr(txtVehicleNo.Text)
            obj.Customer = clsCommon.myCstr(txtCustomerCode.Text)
            obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
            If clsGateEntryReturnPS.SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully")
                End If


                loadData(obj.GE_CODE, NavigatorType.Current)
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
    Sub loadData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsGateEntryReturnPS.GetData(str, navtype)
        If obj IsNot Nothing Then
            Reset()
            isLoadData = True
            fndDocNo.MyReadOnly = True
            isNewEntry = False
            fndDocNo.Value = obj.GE_CODE
            txtDocDate.Text = obj.GE_DATE
            fndRefDocNo.Value = obj.REF_DOC_No
            If clsCommon.myLen(fndRefDocNo.Value) > 0 Then
                txtRefDocDate.Text = clsGateEntryReturnPS.GetRefDocDate(fndRefDocNo.Value, Nothing)
            End If
            txtVehicleNo.Text = obj.Manual_Vehicle
            txtRemarks.Text = obj.Remarks
            txtCustomerCode.Text = obj.Customer
            txtCustomerName.Text = clsGateEntryReturnPS.GetCustomerName(txtCustomerCode.Text, Nothing)
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

                If clsGateEntryReturnPS.DeleteData(fndDocNo.Value) Then
                    myMessages.delete()
                    Reset()
                Else
                    clsCommon.MyMessageBoxShow("Can't delete the record")
                End If
            Else
                clsCommon.MyMessageBoxShow("Please Select a document to delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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
        fndDocNo.Value = clsGateEntryReturnPS.getFinder("", fndDocNo.Value, isButtonClicked)
        loadData(fndDocNo.Value, NavigatorType.Current)
    End Sub

    Private Sub fndDispatchNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRefDocNo._MYValidating
        fndRefDocNo.Value = clsGateEntryReturnPS.getRefDocFinder("Status=1 and trans_type='PS'", fndRefDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndRefDocNo.Value) > 0 Then
            txtRefDocDate.Text = clsGateEntryReturnPS.GetRefDocDate(fndRefDocNo.Value, Nothing)
            txtCustomerCode.Text = clsGateEntryReturnPS.GetCustomerCode(fndRefDocNo.Value, Nothing)
            txtCustomerName.Text = clsGateEntryReturnPS.GetCustomerName(txtCustomerCode.Text, Nothing)
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
                If clsGateEntryReturnPS.PostData(fndDocNo.Value) Then
                    myMessages.post()
                    loadData(fndDocNo.Value, NavigatorType.Current)
                Else
                    clsCommon.MyMessageBoxShow("Can't Post the record")
                End If
            Else
                clsCommon.MyMessageBoxShow("Please Select a document to Post")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
