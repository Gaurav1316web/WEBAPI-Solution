Imports common
Imports System.Data.SqlClient

Public Class FrmJEReverse
    Dim Qry As String
    Dim dt As DataTable
    Dim IsformLoad As Boolean = True
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmJEReverse)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub

    Private Sub FrmJEReverse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gvVouchers.AllowAddNewRow = False
        IsformLoad = False
        dtpMonthYear.Value = clsCommon.GETSERVERDATE
        SetUserMgmtNew()
        
    End Sub
    
    Private Sub LoadData()
        Try
            gvVouchers.DataSource = Nothing
            Qry = "Select Cast(1 as bit) as [Select], Voucher_No, Journal_No, Voucher_Desc, CONVERT(VARCHAR,Voucher_Date,103) as Voucher_Date, Reverse_Date, Source_Doc_No, CustVend_Code, CustVend_Name, Total_Debit_Amt, Total_Credit_Amt  from TSPL_JOURNAL_MASTER WHere Authorized='A' AND Auto_Reverse='Y' "
            Qry += " AND DATEPART(MONTH,COnvert(Date,Reverse_Date,103))=DATEPART(MONTH,Convert(Date,'" + dtpMonthYear.Value + "',103)) "
            Qry += " AND DATEPART(YEAR,COnvert(Date,Reverse_Date,103))=DATEPART(YEAR,Convert(Date,'" + dtpMonthYear.Value + "',103))"
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                lblRecordCounter.Text = "No Record Found"
                btnReverse.Enabled = False
            Else
                gvVouchers.DataSource = dt
                FormatGrid()
                btnReverse.Enabled = True
                lblRecordCounter.Text = "" + clsCommon.myCstr(dt.Rows.Count) + " Record found."
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        gvVouchers.Columns("Select").Width = 50

        gvVouchers.Columns("Voucher_No").Width = 90
        gvVouchers.Columns("Voucher_No").HeaderText = "Voucher No"

        gvVouchers.Columns("Journal_No").Width = 80
        gvVouchers.Columns("Journal_No").HeaderText = "Journal No"

        gvVouchers.Columns("Voucher_Desc").Width = 200
        gvVouchers.Columns("Voucher_Desc").HeaderText = "Decsription"

        gvVouchers.Columns("Voucher_Date").Width = 80
        gvVouchers.Columns("Voucher_Date").HeaderText = "Voucher Date"

        gvVouchers.Columns("Reverse_Date").Width = 80
        gvVouchers.Columns("Reverse_Date").HeaderText = "Reverse Date"

        gvVouchers.Columns("Source_Doc_No").Width = 80
        gvVouchers.Columns("Source_Doc_No").HeaderText = "Source Doc No"

        gvVouchers.Columns("CustVend_Code").Width = 80
        gvVouchers.Columns("CustVend_Code").HeaderText = "Customer/Vendor Code"

        gvVouchers.Columns("CustVend_Name").Width = 200
        gvVouchers.Columns("CustVend_Name").HeaderText = "Customer/Vendor Name"

        gvVouchers.Columns("Total_Debit_Amt").Width = 100
        gvVouchers.Columns("Total_Debit_Amt").HeaderText = "Dr Amount"

        gvVouchers.Columns("Total_Credit_Amt").Width = 100
        gvVouchers.Columns("Total_Credit_Amt").HeaderText = "Cr Amount"
    End Sub

    Private Sub brnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        ReverseData()
    End Sub

    Private Function ReverseData() As Boolean
        Dim trans As SqlTransaction = Nothing
        Dim Counter As Integer = 0
        Try
            trans = clsDBFuncationality.GetTransactin()
            For Each grow As GridViewRowInfo In gvVouchers.Rows
                If grow.Cells("Select").Value = True Then
                    Counter = Counter + 1
                    clsJournalEntryHeader.AutoReverse(clsCommon.myCstr(grow.Cells("Voucher_No").Value), grow.Cells("Reverse_Date").Value, trans, 0)
                End If
            Next

            If Counter <= 0 Then
                clsCommon.MyMessageBoxShow("Please select atleast single voucher to reverse")
            Else
                clsCommon.MyMessageBoxShow("" + clsCommon.myCstr(Counter) + " Vouchers reversed successfully.")
            End If
            trans.Commit()
            LoadData()
            Return True
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Dim PreviousMonth As Integer
    Dim PreviousYear As Integer
    Private Sub dtpMonthYear_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpMonthYear.ValueChanged
        If Not IsformLoad Then
            Dim CurrentMonth As Integer = dtpMonthYear.Value.Month
            Dim CurrentYear As Integer = dtpMonthYear.Value.Year
            If CurrentMonth <> PreviousMonth Or CurrentYear <> PreviousYear Then
                LoadData()
            End If
            PreviousMonth = CurrentMonth
            PreviousYear = CurrentYear
        End If
    End Sub
End Class
