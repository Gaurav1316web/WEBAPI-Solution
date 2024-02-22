Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports Newtonsoft.Json.Linq
Public Class frmBankAdvise
#Region "Variables"
    Dim IsBankAdviseStartDate As String
#End Region
    Private Sub frmBankAdvise_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            IsBankAdviseStartDate = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.BankAdviseRequired, clsFixedParameterCode.BankAdviseRequired, Nothing))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub CreateTable()
    '    Dim coll As Dictionary(Of String, String)
    '    coll = New Dictionary(Of String, String)()
    '    coll.Add("Document_No", "varchar(30) Not NULL Primary key")
    '    coll.Add("Document_Date", "datetime Not NULL")
    '    coll.Add("Payment_Process_Document_No", "varchar(30) Not NULL UNIQUE references TSPL_PAYMENT_PROCESS_HEAD(Doc_No)")
    '    coll.Add("Remarks", "varchar(200) NULL")
    '    coll.Add("Created_By", "varchar(12)  Not NULL")
    '    coll.Add("Created_Date", "datetime  Not NULL")
    '    coll.Add("Modified_By", "varchar(12)  Not NULL")
    '    coll.Add("Modified_Date", "datetime  Not NULL")
    '    coll.Add("Status", "integer NULL")
    '    coll.Add("Posted_By", "varchar(12) NULL")
    '    coll.Add("Posted_Date", "datetime NULL")
    '    clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_BANK_ADVISE", coll, "", True)
    'End Sub

    Private Sub fndDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Try
            Dim Qry As String = "Select Document_No As [Document Code], Document_Date As [Document Date],Case When Status ='' Then 'Pending' Else 'Approved' End As [Status] from TSPL_BANK_ADVISE"
            fndDocNo.Value = clsCommon.ShowSelectForm("fndDocNo", Qry, "Document Code", "", "", "Document_No", isButtonClicked)
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                LoadData(fndDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndDocNo._MYNavigator
        Try
            LoadData(fndDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndPaymentProcessNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPaymentProcessNo._MYValidating
        Try
            fndPaymentProcessNo.Value = clsPaymentProcessHead.getFinder("FarmType='PP'", fndPaymentProcessNo.Value, isButtonClicked)
            If clsCommon.myLen(fndPaymentProcessNo.Value) > 0 Then
                LoadDataPaymentProcessDetails(fndPaymentProcessNo.Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtPPMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtPPMultDCS._My_Click
        Try
            Dim Qry As String = clsBankAdvise.paymentProcessDetails(fndPaymentProcessNo.Value)
            clsCommon.ShowMultipleSelectForm(False, "PPBA", Qry, "DCS Code", "", txtPPMultDCS.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub LoadDataPaymentProcessDetails(paymentProcessDocNo As String)
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(clsBankAdvise.paymentProcessDetails(paymentProcessDocNo))
            If dt.Rows.Count > 0 Then
                txtPPFromDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("From Date"))
                txtPPToDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("To Date"))
                txtMCC.Text = clsCommon.myCstr(dt.Rows(0)("MCC Code"))
                txtPPArea.Text = clsCommon.myCstr(dt.Rows(0)("Area"))
                Dim arrDCS As ArrayList = New ArrayList()
                For Each row In dt.Rows
                    arrDCS.Add(clsCommon.myCstr(row("DCS Code")))
                Next
                txtPPMultDCS.arrValueMember = arrDCS
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        lblPending.Status = ERPTransactionStatus.Pending
        fndDocNo.Value = Nothing
        txtDocDate.Value = clsCommon.GETSERVERDATE()
        fndPaymentProcessNo.Value = Nothing
        txtMCC.Text = Nothing
        txtPPFromDate.Value = clsCommon.GETSERVERDATE()
        txtPPToDate.Value = clsCommon.GETSERVERDATE()
        txtPPMultDCS.arrValueMember = Nothing
        txtPPArea.Text = Nothing
        txtRemarks.Text = Nothing
        btnSave.Text = "Save"
        EnableFeilds()
    End Sub


    Private Sub LoadData(strCode As String, NavType As NavigatorType)
        Try
            Dim obj As clsBankAdvise = clsBankAdvise.GetBankAdviseData(strCode, NavType)
            If obj IsNot Nothing Then
                Reset()
                fndDocNo.Value = obj.Document_No
                txtDocDate.Value = obj.Document_Date
                fndPaymentProcessNo.Value = obj.Payment_Process_Document_No
                If clsCommon.myLen(fndPaymentProcessNo.Value) > 0 Then
                    LoadDataPaymentProcessDetails(fndPaymentProcessNo.Value)
                End If
                If obj.Status > 0 Then
                    lblPending.Status = ERPTransactionStatus.Approved
                Else
                    lblPending.Status = ERPTransactionStatus.Pending
                End If
                txtRemarks.Text = obj.Remarks
                If clsCommon.CompairString(lblPending.Status, ERPTransactionStatus.Pending) = CompairStringResult.Equal Then
                    btnSave.Text = "Update"
                Else
                    DisableFeilds()
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then
                Dim isNewEntry As Boolean = False
                Dim obj As clsBankAdvise = New clsBankAdvise()
                obj.Document_No = fndDocNo.Value
                obj.Document_Date = txtDocDate.Value
                obj.Payment_Process_Document_No = fndPaymentProcessNo.Value
                obj.Remarks = txtRemarks.Text

                If obj.Document_No IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0 Then
                    isNewEntry = False
                Else
                    isNewEntry = True
                End If
                If clsBankAdvise.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, Nothing)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(fndPaymentProcessNo.Value) < 0 Then
            clsCommon.MyMessageBoxShow(Me, "Payment Process Document No. can't be black.", Me.Text)
            Return False
        End If

        If clsCommon.myLen(txtRemarks) < 0 Then
            clsCommon.MyMessageBoxShow(Me, "Remarks can't be black.", Me.Text)
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsBankAdvise.deleteData(fndDocNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully.", Me.Text)
                    Reset()
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsBankAdvise.postData(fndDocNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                    LoadData(fndDocNo.Value, Nothing)
                    DisableFeilds()
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found to Post.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub EnableFeilds()
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
    End Sub

    Public Sub DisableFeilds()
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnPost.Enabled = False
    End Sub


End Class