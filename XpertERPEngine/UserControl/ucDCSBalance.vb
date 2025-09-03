'-------------------BM00000003620--------------------
Imports System.Data.SqlClient
Imports common
Public Class ucDCSBalance
    Private _DCSCode As String = ""
    Private _DCSUploaderCode As String = ""
    Private _VendorCode As String = ""
    Private _DCSName As String = ""
    Private _UnBilledAmt As Double = 0
    Private _TotalOS As Double = 0
    Private _TotalCredit As Double = 0
    Private _TransDate As DateTime
    Public Property DCSCode() As String
        Get
            Return _DCSCode
        End Get
        Set(ByVal value As String)
            _DCSCode = value
        End Set
    End Property
    Public Property TransDate() As DateTime
        Get
            Return _TransDate
        End Get
        Set(ByVal value As DateTime)
            _TransDate = value
        End Set
    End Property
    Public Property DCSUploaderCode() As String
        Get
            Return _DCSUploaderCode
        End Get
        Set(ByVal value As String)
            _DCSUploaderCode = value
        End Set
    End Property
    Public Property DCSName() As String
        Get
            Return _DCSName
        End Get
        Set(ByVal value As String)
            _DCSName = value
        End Set
    End Property
    Public Property VendorCode() As String
        Get
            Return _VendorCode
        End Get
        Set(ByVal value As String)
            _VendorCode = value
        End Set
    End Property
    Public Property UnBilledAmt() As Double
        Get
            Return _UnBilledAmt
        End Get
        Set(ByVal value As Double)
            _UnBilledAmt = value
        End Set
    End Property

    Public Property TotalOS() As Double
        Get
            Return _TotalOS
        End Get
        Set(ByVal value As Double)
            _TotalOS = value
        End Set
    End Property

    Public Property TotalCredit() As Double
        Get
            Return _TotalCredit
        End Get
        Set(ByVal value As Double)
            _TotalCredit = value
        End Set
    End Property
    Public Sub RefreshData(Optional ByVal trans As SqlTransaction = Nothing)
        Try
            RadGroupBox1.Text = ""
            lblUnbilledAmt.Text = "0"
            lblTotalOS.Text = "0"
            lblTotalCredit.Text = "0"
            If clsCommon.myCstr(_DCSUploaderCode).Contains("'") Then
                _DCSUploaderCode = clsCommon.myCstr(_DCSUploaderCode.Replace("'", ""))
            End If
            If clsCommon.myLen(_DCSUploaderCode) > 0 Then
                RadGroupBox1.Text = _DCSUploaderCode + " ( " + _DCSName + " )"
                Dim qry As String = " select isnull(sum(TSPL_MILK_SRN_DETAIL.AMOUNT),0) as Srn_Amt from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE= TSPL_MILK_SRN_DETAIL.DOC_CODE left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE= TSPL_MILK_SRN_DETAIL.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is null and TSPL_MILK_SRN_HEAD.VLC_CODE='" + _DCSCode + "'  and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' "
                lblUnbilledAmt.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                qry = "  select sum(Balance_Amt)Balance_Amt from ( select  isnull(TSPL_VENDOR_INVOICE_HEAD.Balance_Amt,0) as Balance_Amt,TSPL_MULTIPLE_DEDUCTION_DETAIL.Vendor_Code from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_VENDOR_INVOICE_HEAD.Document_No left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
		        where  TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1 and TSPL_MULTIPLE_DEDUCTION_DETAIL.Vendor_Code = '" + _VendorCode + "' and TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "'   	
						union all
			select isnull(TSPL_Customer_Invoice_head.Balance_Amt,0) as Balance_Amt,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_Customer_Invoice_head.Customer_Code
			where Trans_Type = 'MCC' and TSPL_Customer_Invoice_Head.Status=1 and TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code= '" + _VendorCode + "' and TSPL_Customer_Invoice_Head.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' )xx group by Vendor_Code  "
                lblTotalOS.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                lblTotalCredit.Text = clsCommon.myCdbl(lblUnbilledAmt.Text - lblTotalOS.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
