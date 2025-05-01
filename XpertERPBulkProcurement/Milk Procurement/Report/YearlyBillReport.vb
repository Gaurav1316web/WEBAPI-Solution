Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports Telerik.WinControls.UI.Export

Public Class YearlyBillReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub YearlyBillReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim PPDocNo As String = ""
            Dim Document As String = Nothing
            Dim qry As String = ""
            Dim Description As String = Nothing
            Dim DescName As String = Nothing
            Dim DescName1 As String = Nothing
            Dim DescName2 As String = Nothing
            Dim DescName3 As String = Nothing
            Dim DescName4 As String = Nothing
            PPDocNo = " Select Doc_No from TSPL_PAYMENT_PROCESS_HEAD where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date ,103)>=convert(date,'" & fromDate.Value & "',103) 
                        And convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date ,103)<=convert(date,'" & ToDate.Value & "',103) "
            Dim dtDoc As DataTable = clsDBFuncationality.GetDataTable(PPDocNo)
            If dtDoc.Rows.Count > 0 Then
                For i As Integer = 0 To dtDoc.Rows.Count - 1
                    If i = 0 Then
                        Document += " '" + clsCommon.myCstr(dtDoc.Rows(i)("Doc_No")) + "' "
                    Else
                        Document += ", '" + clsCommon.myCstr(dtDoc.Rows(i)("Doc_No")) + "' "
                    End If
                Next
            End If

            qry = " Select distinct Ded_Code,Ded_Desc from (select TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
from TSPL_PAYMENT_PROCESS_DEDUCTION 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_no=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_no
where TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No In (" & Document & ")
UNION ALL
select case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as Ded_Code , case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as Ded_Desc 
from TSPL_PAYMENT_PROCESS_CREDIT_NOTE 
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No 
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No In (" & Document & ")
union all
select  case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as Ded_Code , case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as Ded_Desc
from TSPL_PAYMENT_PROCESS_SAVING 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No 
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where TSPL_PAYMENT_PROCESS_SAVING.Doc_No In (" & Document & ")
union all
select  case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as Ded_Code , case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as Ded_Desc
from TSPL_PAYMENT_PROCESS_COMPULSORY 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No 
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No In (" & Document & ")
union all
select  case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as Ded_Code , case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as Ded_Desc
from TSPL_PAYMENT_PROCESS_MCC_SALE 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No 
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No In (" & Document & ")
)xx "
            Dim dtDesc As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtDesc.Rows.Count > 0 Then
                For i As Integer = 0 To dtDesc.Rows.Count - 1
                    Dim J As Integer = 0
                    If i = 0 Then
                        J = i
                        'Description += " '" + clsCommon.myCstr(dtDesc.Rows(i)("Doc_No")) + "' "
                        Description += "[A]," + "[" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "] "
                        DescName += "0 as [A]," + " 0 as [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + "] "
                        DescName2 += " isnull ([A], 0)  as [A], IsNull([" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "],0) As [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "]"
                        DescName1 += " sum(isnull ([A], 0))  as [A] ,Sum(IsNull([" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "],0)) As [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "]"
                        DescName3 += " sum(isnull ([A], 0))  as [A] ,Sum(IsNull([" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "],0)) As [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + clsCommon.myCstr(J) + "]"
                        DescName4 += " SUM([A]) AS [A],Sum([" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + clsCommon.myCstr(J) + "]) as [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + "] "
                        'DescName4 += " SUM([A]) AS [A],Sum[" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + clsCommon.myCstr(J) + "] as [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + clsCommon.myCstr(J) + "] "
                    Else
                        J = +i
                        Description += ", [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "] "
                        DescName += ",  0 as [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + "] "
                        DescName2 += " , IsNull([" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "],0) As [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "]"
                        DescName1 += " ,Sum(IsNull([" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "],0)) As [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "]"
                        DescName3 += " ,Sum(IsNull([" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Code")) + "],0)) As [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + clsCommon.myCstr(J) + "]"
                        DescName4 += " ,Sum([" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + clsCommon.myCstr(J) + "]) as [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + "] "
                        'DescName4 += " SUM([A]) AS [A],Sum[" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + clsCommon.myCstr(J) + "] as [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + clsCommon.myCstr(J) + "] "
                        'DescName1 += " ,Sum(IsNull([" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + "],0)) As [" + clsCommon.myCstr(dtDesc.Rows(i)("Ded_Desc")) + J"]"


                    End If
                Next
            End If
            Dim Qry1 As String = " Select
				                    TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,'' as Gender,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,
									TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else  TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as DCS_Addition_Deduction,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as DCSDescription,
                                    TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,TSPL_VENDOR_INVOICE_DETAIL.Amount as VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date
									from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
					                  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code = TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
									left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode
									 Union all	
									 
									SELECT TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,'' as Gender,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,
									TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as DCS_Addition_Deduction,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as DCSDescription,
                                    TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,TSPL_VENDOR_INVOICE_DETAIL.Amount as VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No ,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date
								   FROM TSPL_PAYMENT_PROCESS_DEDUCTION
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
									left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
									left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode
									union all

									SELECT TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,'' as Gender,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,
									TSPL_PAYMENT_PROCESS_SAVING.Doc_No,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as DCS_Addition_Deduction,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as DCSDescription,
                                    TSPL_VENDOR_INVOICE_DETAIL.Amount as Amount,0 AS VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date ,TSPL_PAYMENT_PROCESS_HEAD.To_Date
									FROM TSPL_PAYMENT_PROCESS_SAVING
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_SAVING.Doc_No
									left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
								    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
								    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
									union all

									SELECT TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,'' as Gender,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,
									
									TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No,
                                    case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as DCS_Addition_Deduction,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as DCSDescription,
                                    TSPL_VENDOR_INVOICE_DETAIL.Amount as Amount,0 AS VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date 
									FROM TSPL_PAYMENT_PROCESS_COMPULSORY
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No
									left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
								    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
								    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
									
									union all
									SELECT TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,'' as Gender,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,
									TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No,
                                    case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as DCS_Addition_Deduction,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as DCSDescription,
                                    TSPL_VENDOR_INVOICE_DETAIL.Amount as Amount,0 AS VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date 
									FROM TSPL_PAYMENT_PROCESS_MCC_SALE
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No
									left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
								    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
								    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code

									
									union all
									Select TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,TSPL_VENDOR_MASTER.Gender,VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,VSP_NAME,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,
                                   '' as DCS_Addition_Deduction,'' as DCSDescription,
                                    0 as Amount,0 AS VendorAmt,'' as InvoiceNo,'' as Main_VSP_Milk_AP_Invoice_No ,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date
								   from TSPL_PAYMENT_PROCESS_DETAIL
                                   left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                   left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
								   left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code "



            Dim sQuery As String = Nothing
            If rdbSummary.IsChecked = True Then
                sQuery = "  Select VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                                    from 
                                   (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from (Select max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " 
                                    from(Select Registered_PDCS_CLUSTER,Gender,VSP_CODE,DCSCode,VSP_NAME,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " 
                                     from (Select max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount 
                                   from (   Select max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME,Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date 
                                   from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    sQuery += " and Vendor_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                End If
                sQuery += " group by xx.Doc_No,xx.Vendor_CODE,xx.DCS_Addition_Deduction )YY group by VSP_CODE,DCS_Addition_Deduction)Tab1 
                        PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by VSP_CODE )YY 

                        Union all
									Select '' as Registered_PDCS_CLUSTER,'' as Gender,VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                                    0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & "  ,																
									Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty
                                    from (SELECT XX.DOC_CODE,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,SUM(XX.Qty)Qty,
                                    XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                                    CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty 
                                    FROM (Select TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                                    TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD from TSPL_MILK_PURCHASE_INVOICE_DETAIL
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                                    left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                    left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                                    LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No  
                                    where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                                    and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103) "
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                End If
                sQuery += " )XX GROUP BY DOC_CODE,QBD)yy group by VSP_CODE )Tab2 group by VSP_CODE "

            ElseIf rdbMonth.IsChecked = True Then
                sQuery = "  Select max(Month_Name)Month_Name,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                            from (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty 
                                   from (Select max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " ,max(Month_Name)Month_Name,Month_Number
                            from (Select Registered_PDCS_CLUSTER,Gender,VSP_CODE,DCSCode,VSP_NAME,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " ,Month_Name,Month_Number
                                   
								   from (Select max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(Month_Name)Month_Name,Month_Number
                                   
								   from (   Select max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME,Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date,
                                   DATENAME(MONTH, MAX(From_Date)) AS Month_Name,MONTH(MAX(From_Date)) AS Month_Number
                                   from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    sQuery += " and Vendor_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                End If
                sQuery += " group by xx.Doc_No,xx.Vendor_CODE,xx.DCS_Addition_Deduction)YY group by Month_Number,DCS_Addition_Deduction) Tab1
                            PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp
									group by Month_Number )YY

                    Union all
                             Select '' as Registered_PDCS_CLUSTER,'' as Gender,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                             0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & ",max(Month_Name)Month_Name,Month_Number,Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty
                              from (SELECT XX.DOC_CODE,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,SUM(XX.Qty)Qty,
                                    XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                                    CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty,max(Month_Name)Month_Name,Month_Number 
                                    FROM (Select TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                                    TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD ,
									DATENAME(MONTH, (DOC_DATE)) AS Month_Name,MONTH((DOC_DATE)) AS Month_Number
									
									from TSPL_MILK_PURCHASE_INVOICE_DETAIL
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                                    left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                    left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                                    LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No 
                                    where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                                    and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103) "
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                End If
                sQuery += " )XX GROUP BY DOC_CODE,QBD,Month_Number)yy group by Month_Number )Tab2 group by Month_Number "

            ElseIf rdbCycleW.IsChecked = True Then
                sQuery = " Select FORMAT(MAX(From_Date), 'dd-MM') + ' to ' + FORMAT(To_Date, 'dd-MM') AS Date_Range
                           ,To_Date,max(From_Date)From_Date,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                                    from 
                                   (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from 
                                   (Select max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " ,To_Date ,max(From_Date)From_Date 
                                   from(Select Registered_PDCS_CLUSTER,Gender,VSP_CODE,DCSCode,VSP_NAME,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " ,From_Date,To_Date 
                                   from (Select max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,max(From_Date)From_Date ,(To_Date)To_Date
                                   from (   Select max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME,
                                   Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date ,
                                   max(To_Date)To_Date from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    sQuery += " and Vendor_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                End If
                sQuery += " group by xx.Doc_No,xx.Vendor_CODE,xx.DCS_Addition_Deduction )YY group by To_Date,DCS_Addition_Deduction)Tab1  
                            PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by To_Date )YY 
                        
                            Union all
						    Select '' as Registered_PDCS_CLUSTER,'' as Gender,MAX(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                                    0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & ",DOC_DATE,'' as From_Date,																
									Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty FROM (SELECT XX.DOC_CODE,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,SUM(XX.Qty)Qty,
                                    XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                                    CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty,MAX(DOC_DATE)DOC_DATE 
                                    FROM (Select TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                                    TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE
									
									from TSPL_MILK_PURCHASE_INVOICE_DETAIL
									LEFT OUTER JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD ON TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                                    left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                    left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                                    LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
                                    where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                                    and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103)"
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                End If
                sQuery += " )XX GROUP BY DOC_CODE,QBD ) XX GROUP BY DOC_DATE)Tab2 group by To_Date "

            ElseIf rdbMonthCycle.IsChecked = True Then
                sQuery = "  WITH BaseData AS ( Select DATENAME(MONTH, max(From_Date)) AS Month_Name,MONTH(max(From_Date)) AS Month_Number,FORMAT(MAX(From_Date), 'dd-MM') + ' to ' + FORMAT(To_Date, 'dd-MM') AS Date_Range
                           ,To_Date,max(From_Date)From_Date,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                                    from 
                                   (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from 
                                   (Select max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " ,To_Date ,max(From_Date)From_Date 
                                   from(Select Registered_PDCS_CLUSTER,Gender,VSP_CODE,DCSCode,VSP_NAME,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " ,From_Date,To_Date 
                                   from (Select max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,max(From_Date)From_Date ,(To_Date)To_Date
                                   from (   Select max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME,
                                   Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date ,
                                   max(To_Date)To_Date from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    sQuery += " and Vendor_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                End If
                sQuery += " group by xx.Doc_No,xx.Vendor_CODE,xx.DCS_Addition_Deduction )YY group by To_Date,DCS_Addition_Deduction)Tab1  
                            PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by To_Date )YY 
                        
                            Union all
						    Select '' as Registered_PDCS_CLUSTER,'' as Gender,MAX(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                                    0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & ",DOC_DATE,'' as From_Date,																
									Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty FROM (SELECT XX.DOC_CODE,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,SUM(XX.Qty)Qty,
                                    XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                                    CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty,MAX(DOC_DATE)DOC_DATE 
                                    FROM (Select TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                                    TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE
									
									from TSPL_MILK_PURCHASE_INVOICE_DETAIL
									LEFT OUTER JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD ON TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                                    left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                    left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                                    LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
                                    where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                                    and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103)"
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                End If
                sQuery += " )XX GROUP BY DOC_CODE,QBD ) XX GROUP BY DOC_DATE)Tab2 group by To_Date)
SELECT * FROM BaseData
Union all
SELECT 
    NULL AS Month_Name,Month_Number,'Total of ' + DATENAME(MONTH, DATEFROMPARTS(YEAR(max(To_Date)), Month_Number, 1)) AS Date_Range,
    NULL AS To_Date,NULL AS From_Date,MAX(VSP_CODE) AS VSP_CODE,MAX(DCSCode) AS DCSCode,MAX(VSP_NAME) AS VSP_NAME,MAX(Registered_PDCS_CLUSTER) AS Registered_PDCS_CLUSTER,
    MAX(Gender) AS Gender,SUM(Milk_Qty) AS Milk_Qty,SUM(Milk_Amount) AS Milk_Amount,SUM(Head_Load_Amount) AS Head_Load_Amount,SUM(Deduction_Amount) AS Deduction_Amount,
    SUM(Credit_Note_Amount) AS Credit_Note_Amount," & DescName4 & ", SUM(SweetQty) AS SweetQty,SUM(SourQty) AS SourQty,SUM(CurdQty) AS CurdQty,SUM(Payable_Amount) AS Payable_Amount
FROM BaseData GROUP BY Month_Number ORDER BY Month_Number, Date_Range "
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                SetGridFormation()
                RadPageView2.SelectedPage = RadPageViewPage5
                gv1.BestFitColumns()
                EnableDisableControls(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).VisibleInColumnChooser = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rdbSummary.IsChecked = True Then
            gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
            gv1.Columns("VSP_CODE").IsVisible = False
            gv1.Columns("VSP_CODE").VisibleInColumnChooser = True

            gv1.Columns("DCSCode").HeaderText = "DCS Code"
            gv1.Columns("VSP_NAME").HeaderText = "DCS NAME"
            gv1.Columns("Registered_PDCS_CLUSTER").HeaderText = "DCS Type"
            gv1.Columns("Gender").HeaderText = "Gender"
            gv1.Columns("Milk_Qty").HeaderText = "Milk Qty"
            gv1.Columns("Milk_Amount").HeaderText = "Milk Purchase"
            gv1.Columns("Head_Load_Amount").HeaderText = "Head Load"
            gv1.Columns("Payable_Amount").HeaderText = "Net Payable"
            gv1.Columns("Deduction_Amount").HeaderText = "Deduction Amount"
            gv1.Columns("Deduction_Amount").IsVisible = False
            gv1.Columns("Deduction_Amount").VisibleInColumnChooser = True
            gv1.Columns("Credit_Note_Amount").HeaderText = "Credit Note Amount"
            gv1.Columns("Credit_Note_Amount").IsVisible = False
            gv1.Columns("Credit_Note_Amount").VisibleInColumnChooser = True
            gv1.Columns("A").HeaderText = "A"
            gv1.Columns("A").IsVisible = False
            gv1.Columns("A").VisibleInColumnChooser = True

            Dim index As Integer = 5
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbMonth.IsChecked = True Then
            gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
            gv1.Columns("VSP_CODE").IsVisible = False
            gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
            gv1.Columns("DCSCode").IsVisible = False
            gv1.Columns("VSP_NAME").IsVisible = False
            gv1.Columns("Registered_PDCS_CLUSTER").IsVisible = False
            gv1.Columns("Gender").IsVisible = False
            gv1.Columns("Milk_Qty").HeaderText = "Milk Qty"
            gv1.Columns("Milk_Amount").HeaderText = "Milk Purchase"
            gv1.Columns("Head_Load_Amount").HeaderText = "Head Load"
            gv1.Columns("Payable_Amount").HeaderText = "Net Payable"
            gv1.Columns("Deduction_Amount").HeaderText = "Deduction Amount"
            gv1.Columns("Deduction_Amount").IsVisible = False
            gv1.Columns("Deduction_Amount").VisibleInColumnChooser = True
            gv1.Columns("Credit_Note_Amount").HeaderText = "Credit Note Amount"
            gv1.Columns("Credit_Note_Amount").IsVisible = False
            gv1.Columns("Credit_Note_Amount").VisibleInColumnChooser = True
            gv1.Columns("A").HeaderText = "A"
            gv1.Columns("A").IsVisible = False
            gv1.Columns("A").VisibleInColumnChooser = True
            gv1.Columns("Month_Name").HeaderText = "Month"
            Dim index As Integer = 6
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbCycleW.IsChecked = True Then
            gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
            gv1.Columns("VSP_CODE").IsVisible = False
            gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
            gv1.Columns("DCSCode").IsVisible = False
            gv1.Columns("VSP_NAME").IsVisible = False
            gv1.Columns("Registered_PDCS_CLUSTER").IsVisible = False
            gv1.Columns("Gender").IsVisible = False
            gv1.Columns("Milk_Qty").HeaderText = "Milk Qty"
            gv1.Columns("Milk_Amount").HeaderText = "Milk Purchase"
            gv1.Columns("Head_Load_Amount").HeaderText = "Head Load"
            gv1.Columns("Payable_Amount").HeaderText = "Net Payable"
            gv1.Columns("Deduction_Amount").HeaderText = "Deduction Amount"
            gv1.Columns("Deduction_Amount").IsVisible = False
            gv1.Columns("Deduction_Amount").VisibleInColumnChooser = True
            gv1.Columns("Credit_Note_Amount").HeaderText = "Credit Note Amount"
            gv1.Columns("Credit_Note_Amount").IsVisible = False
            gv1.Columns("Credit_Note_Amount").VisibleInColumnChooser = True
            gv1.Columns("A").HeaderText = "A"
            gv1.Columns("A").IsVisible = False
            gv1.Columns("A").VisibleInColumnChooser = True
            gv1.Columns("To_Date").IsVisible = False
            gv1.Columns("To_Date").VisibleInColumnChooser = True
            gv1.Columns("From_Date").IsVisible = False
            gv1.Columns("From_Date").VisibleInColumnChooser = True
            gv1.Columns("Date_Range").HeaderText = "Cycle Wise"
            Dim index As Integer = 6
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbMonthCycle.IsChecked = True Then
            gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
            gv1.Columns("VSP_CODE").IsVisible = False
            gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
            gv1.Columns("DCSCode").IsVisible = False
            gv1.Columns("VSP_NAME").IsVisible = False
            gv1.Columns("Registered_PDCS_CLUSTER").IsVisible = False
            gv1.Columns("Gender").IsVisible = False
            gv1.Columns("Milk_Qty").HeaderText = "Milk Qty"
            gv1.Columns("Milk_Amount").HeaderText = "Milk Purchase"
            gv1.Columns("Head_Load_Amount").HeaderText = "Head Load"
            gv1.Columns("Payable_Amount").HeaderText = "Net Payable"
            gv1.Columns("Deduction_Amount").HeaderText = "Deduction Amount"
            gv1.Columns("Deduction_Amount").IsVisible = False
            gv1.Columns("Deduction_Amount").VisibleInColumnChooser = True
            gv1.Columns("Credit_Note_Amount").HeaderText = "Credit Note Amount"
            gv1.Columns("Credit_Note_Amount").IsVisible = False
            gv1.Columns("Credit_Note_Amount").VisibleInColumnChooser = True
            gv1.Columns("A").HeaderText = "A"
            gv1.Columns("A").IsVisible = False
            gv1.Columns("A").VisibleInColumnChooser = True
            gv1.Columns("Month_Name").IsVisible = False
            gv1.Columns("Month_Number").IsVisible = False
            gv1.Columns("To_Date").IsVisible = False
            gv1.Columns("From_Date").IsVisible = False
            gv1.Columns("Date_Range").HeaderText = "Monthly Cycle"
            'Dim index As Integer = 11

            'For ii As Integer = index To gv1.Columns.Count - 1
            '    summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            'Next

        End If

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        txtDCS.arrValueMember = Nothing
        RadPageView2.SelectedPage = RadPageViewPage4
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        txtDCS.Enabled = val
        RadGroupBox11.Enabled = val
        RadGroupBox6.Enabled = val
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(objCommonVar.CurrentCompanyName)
                clsCommon.MyExportToExcelGrid("", gv1, arrHeader, Me.Text)
                'transportSql.exportdata(gv1, "", Me.Text, False, arrHeader, False, False, True)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(objCommonVar.CurrentCompanyName)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDCS__My_Click(sender As Object, e As EventArgs) Handles txtDCS._My_Click
        Try
            Dim qry As String = " select  TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code] ,TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name],TSPL_VLC_MASTER_HEAD.VSP_Code,isnull(TSPL_VENDOR_MASTER.Zone_Code,'') AS Zone
		                          from TSPL_VLC_MASTER_HEAD 
		                          left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code"

            txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "VSP_Code", "DCS Name", txtDCS.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class