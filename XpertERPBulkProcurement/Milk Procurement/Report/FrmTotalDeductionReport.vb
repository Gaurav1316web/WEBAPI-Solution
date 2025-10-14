Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient

Public Class FrmTotalDeductionReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim dt As DataTable
    Dim isLoad As Boolean = False

#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
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
            PPDocNo = " Select Doc_No from TSPL_PAYMENT_PROCESS_HEAD where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date ,103)>=convert(date,'" & txtFromDate.Value & "',103) 
                        And convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date ,103)<=convert(date,'" & txtToDate.Value & "',103) "
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

            If dtDoc.Rows.Count > 0 Then

                qry = " Select distinct Ded_Code,Ded_Desc from ( 
select COALESCE(TSPL_DEDUCTION_MASTER.Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code ) AS Ded_Code,
COALESCE(TSPL_DEDUCTION_MASTER.Description,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc )  as Ded_Desc  
from TSPL_PAYMENT_PROCESS_DEDUCTION 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_no=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_no
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction
where "
                qry += " TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' And"
                qry += " TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No In (" & Document & ")
union all
select  case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as Ded_Code , case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as Ded_Desc
from TSPL_PAYMENT_PROCESS_MCC_SALE 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No 
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where "

                qry += " TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' And"
                qry += " TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No In (" & Document & ")
)xx where 2=2 and Ded_Code is not null "
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
            End If

            If dtDoc.Rows.Count > 0 Then
                Dim Qry1 As String = "   									 
									SELECT TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,'' as Gender,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,
									TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No,COALESCE(TSPL_DEDUCTION_MASTER.Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code) AS DCS_Addition_Deduction,
									 COALESCE(TSPL_DEDUCTION_MASTER.Description, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)  as DCSDescription,
                                    TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,TSPL_VENDOR_INVOICE_DETAIL.Amount as VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No ,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName
								   FROM TSPL_PAYMENT_PROCESS_DEDUCTION
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
									left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
									left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
									left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
                                    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
								    left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code

                                    union all
    
									SELECT TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,'' as Gender,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,
									TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No,
                                    TSPL_DEDUCTION_MASTER.Code as DCS_Addition_Deduction,TSPL_DEDUCTION_MASTER.Description as DCSDescription,
                                    TSPL_VENDOR_INVOICE_DETAIL.Amount as Amount,0 AS VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName 
									FROM TSPL_PAYMENT_PROCESS_MCC_SALE
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No
									left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
								    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction 
								    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
									left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
	                                left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code"



                Dim sQuery As String = ""
                sQuery = " Select  "
                If rdbDCS.IsChecked Then
                    sQuery += " DCSCode,[DCS Name] "
                ElseIf rdbBMC.IsChecked Then
                    sQuery += " MCC,MCC_NAME as [MCC Name] "
                ElseIf rdbArea.IsChecked Then
                    sQuery += "Area_Location_Code as Area,Location_Desc as [Area Name]"
                End If
                sQuery += " ,(DeductionName) as DeductionName
,cast((OP) as  decimal(18,2)) as [Opening],Total_Deduction,Deduction_Consumed as Deduction_Consumed,
cast((OP+Total_Deduction-Deduction_Consumed) as decimal(18,2)) as [Balance Amount] 
from ("
                If rdbDCS.IsChecked Then
                    sQuery += " select max(Area_Location_Code)Area_Location_Code,max(Location_Desc)Location_Desc,max(MCC)MCC,max(MCC_NAME)MCC_NAME,xx.DeductionCode,COALESCE(MAX(TSPL_DCS_ADDITION_DEDUCTION.Description),MAX(TSPL_DEDUCTION_MASTER.Description))  as DeductionName,
                TSPL_VENDOR_MASTER.Vendor_Code,max(VLC_Code_VLC_Uploader) as DCSCode,max(TSPL_VENDOR_MASTER.Vendor_Name) as [DCS Name]"

                ElseIf rdbBMC.IsChecked Then
                    sQuery += "select max(Area_Location_Code)Area_Location_Code,max(Location_Desc)Location_Desc,MCC,max(MCC_NAME)MCC_NAME,xx.DeductionCode,COALESCE(MAX(TSPL_DCS_ADDITION_DEDUCTION.Description),MAX(TSPL_DEDUCTION_MASTER.Description))  as DeductionName,
                max(TSPL_VENDOR_MASTER.Vendor_Code)Vendor_Code,max(VLC_Code_VLC_Uploader) as DCSCode,max(TSPL_VENDOR_MASTER.Vendor_Name) as [DCS Name]"
                ElseIf rdbArea.IsChecked Then
                    sQuery += "select (Area_Location_Code)Area_Location_Code,max(Location_Desc)Location_Desc,max(MCC)MCC,max(MCC_NAME)MCC_NAME,xx.DeductionCode,COALESCE(MAX(TSPL_DCS_ADDITION_DEDUCTION.Description),MAX(TSPL_DEDUCTION_MASTER.Description))  as DeductionName,
                TSPL_VENDOR_MASTER.Vendor_Code,max(VLC_Code_VLC_Uploader) as DCSCode,max(TSPL_VENDOR_MASTER.Vendor_Name) as [DCS Name]"
                End If

                sQuery += " 
,sum((Amount-Reduce_Deduc_Amt) * (case when  Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=2 or RI=1 or RI=3 or RI=4 then 1 else -1 end)) as OP 
,sum(Amount * (case when  Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when (RI=1 or RI=3 or RI=5) then 1 else 0 end)) as Total_Deduction
,sum((Amount-Reduce_Deduc_Amt) * (case when Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when (RI=2 or RI=4) then 1 else 0 end)) as Deduction_Consumed 
,max(Active)Active
from (select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo as AP_Invoice_No,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,
TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount,0 as Reduce_Deduc_Amt ,1 as RI,TSPL_VLC_MASTER_HEAD.Active
from TSPL_MULTIPLE_DEDUCTION_DETAIL
left  join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo
left  join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
where 2=2 

Union all
select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as DeductionCode,
TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,
TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,2 as RI,TSPL_VLC_MASTER_HEAD.Active   
from TSPL_PAYMENT_PROCESS_DEDUCTION
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
where 2=2
Union all
select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_SD_SHIPMENT_HEAD.Document_Code as Document_No,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_Customer_Invoice_Head.Document_No as AP_Invoice_No ,TSPL_Customer_Invoice_Head.Posting_Date as AP_Invoice_Date,'D' as Document_Type,TSPL_SD_SHIPMENT_HEAD.Deduction as DeductionCode,TSPL_VLC_MASTER_HEAD.VSP_Code as Vendor_Code ,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ,TSPL_SD_SHIPMENT_HEAD.Total_Amt as Amount,0 as Reduce_Deduc_Amt ,3 as RI,TSPL_VLC_MASTER_HEAD.Active 
from  TSPL_SD_SHIPMENT_HEAD 
left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_SD_SHIPMENT_HEAD.Deduction 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VLC_MASTER_HEAD.VSP_Code
left outer join TSPL_CUSTOMER_INVOICE_HEAD on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No
left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
where  TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and TSPL_SD_SHIPMENT_HEAD.is_cashsale='N'  and TSPL_SD_SHIPMENT_HEAD.Status=1
union all
select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No as AP_Invoice_No ,TSPL_Customer_Invoice_Head.Posting_Date as AP_Invoice_Date,'D' as Document_Type,TSPL_SD_SHIPMENT_HEAD.Deduction as DeductionCode,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount,TSPL_PAYMENT_PROCESS_MCC_SALE.Reduce_Deduc_Amt,4 as RI,TSPL_VLC_MASTER_HEAD.Active
from TSPL_PAYMENT_PROCESS_MCC_SALE
left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_PAYMENT_PROCESS_MCC_SALE.Shipment_Doc_No
left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No
left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
where 2=2 
union all
select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as DeductionCode,
TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,
TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,5 as RI,TSPL_VLC_MASTER_HEAD.Active   
from TSPL_PAYMENT_PROCESS_DEDUCTION
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code 
left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
where 2=2 and ISProcurementDeduction=0
 )xx
left  join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=xx.DeductionCode
left  join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=xx.DeductionCode
left  join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code  "
                If rdbDCS.IsChecked Then
                    sQuery += " group by DeductionCode,TSPL_VENDOR_MASTER.Vendor_Code "
                ElseIf rdbBMC.IsChecked Then
                    sQuery += " group by DeductionCode,MCC "
                ElseIf rdbArea.IsChecked Then
                    sQuery += "group by DeductionCode,Area_Location_Code"
                End If

                sQuery += " )xxx  where 2=2 "

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
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                    EnableDisableControls(False)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
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
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            funreset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        'txtDCS.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage2
        'If AreaWiseBilling Then
        '    rdbArea.Visible = True
        'Else
        '    rdbArea.Visible = False
        'End If
    End Sub

    'Private Sub EnableDisableControls(ByVal val As Boolean)
    '    txtDCS.Enabled = val
    '    txtMCC.Enabled = val
    '    TxtDeduction.Enabled = val
    '    RadGroupBox1.Enabled = val
    'End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
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

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
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

    Private Sub txtMCC_Click(sender As Object, e As EventArgs) Handles txtMCC.Click
        Try
            Dim qry As String = " Select MCC_Code,MCC_NAME from TSPL_MCC_MASTER "

            txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmTotalDeductionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
    End Sub

    Private Sub txtFromDate_Leave(sender As Object, e As EventArgs) Handles txtFromDate.Leave
        If rbtnMonthly.Checked Then
            SetToDateMonthly()
        ElseIf rbtnCycleWise.Checked Then
            SetToDateNew()
        Else
            txtToDate.Value = clsCommon.GETSERVERDATE
        End If

    End Sub

    Sub SetToDateMonthly()
        Try
            'If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
            'clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month", Me.Text)
            Dim dtCurr As Date = (txtFromDate.Value)
                txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1).ToString("dd/MM/yyyy")

                ' Set ToDate = last day of month
                txtToDate.Value = New Date(dtCurr.Year, dtCurr.Month, Date.DaysInMonth(dtCurr.Year, dtCurr.Month)).ToString("dd/MM/yyyy")

                'txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                ' txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetToDateNew()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select top 1 PC_VALUE,PC_TYPE from TSPL_PAYMENT_CYCLE_MASTER ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    txtToDate.Value = txtFromDate.Value
                    Exit Sub
                End If
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

                If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Value.Month <> dtNxtPay.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = txtFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                txtFromDate.Value = today.AddDays(-dayDiff)
                txtToDate.Value = txtFromDate.Value.AddDays(6)
            End If
        End If
    End Sub

    Private Sub txtFromDate_Validated(sender As Object, e As EventArgs) Handles txtFromDate.Validated
        If rbtnMonthly.Checked Then
            SetToDateMonthly()
        ElseIf rbtnCycleWise.Checked Then
            SetToDateNew()
        End If
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

    Private Sub TxtDeduction__My_Click(sender As Object, e As EventArgs) Handles TxtDeduction._My_Click
        Try
            Dim qry As String = " Select Code,Description from TSPL_DEDUCTION_MASTER
                                  union all
                                  Select code,description from TSPL_DCS_ADDITION_DEDUCTION "

            TxtDeduction.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "Code", "Description", TxtDeduction.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        txtDCS.Enabled = val
        RadGroupBox2.Enabled = val
        RadGroupBox3.Enabled = val
        RadGroupBox1.Enabled = val
    End Sub
End Class