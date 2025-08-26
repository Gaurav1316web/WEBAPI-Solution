Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports Telerik.WinControls.UI.Export

Public Class YearlyBillReport
    Inherits FrmMainTranScreen
#Region "Variable"
    Dim AreaWiseBilling As Boolean = False
    Dim StrPermission As String
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub YearlyBillReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AreaWiseBilling = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
            funreset()
            fromDate.Value = clsCommon.GETSERVERDATE()
            ToDate.Value = clsCommon.GETSERVERDATE()
            StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
            txtMultArea.Visible = AreaWiseBilling
            lblArea.Visible = AreaWiseBilling
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            'Dim isDedfound As Integer = 0
            'isDedfound = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select count(*) from TSPL_DCS_ADDITION_DEDUCTION WHERE Deduction IS NULL "))
            'If isDedfound > 0 Then
            '    common.clsCommon.MyMessageBoxShow(Me, "Please Map remaining Code", Me.Text)
            '    Exit Sub
            'End If

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

            If dtDoc.Rows.Count > 0 Then

                qry = " Select distinct Ded_Code,Ded_Desc from ( 
select COALESCE(TSPL_DEDUCTION_MASTER.Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code ) AS Ded_Code,
COALESCE(TSPL_DEDUCTION_MASTER.Description,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc )  as Ded_Desc  
from TSPL_PAYMENT_PROCESS_DEDUCTION 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_no=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_no
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction
where TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No In (" & Document & ")
UNION ALL

select  COALESCE(TSPL_DEDUCTION_MASTER.Code, TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode) AS Ded_Code,
COALESCE(TSPL_DEDUCTION_MASTER.Description, TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc)  as Ded_Desc
from TSPL_PAYMENT_PROCESS_CREDIT_NOTE 
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No 
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
left join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No where TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No In (" & Document & ")
union all
select  TSPL_DEDUCTION_MASTER.Code as Ded_Code,TSPL_DEDUCTION_MASTER.Description as Ded_Desc
from TSPL_PAYMENT_PROCESS_SAVING 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No 
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where TSPL_PAYMENT_PROCESS_SAVING.Doc_No In (" & Document & ")
union all
select  TSPL_DEDUCTION_MASTER.Code as Ded_Code,TSPL_DEDUCTION_MASTER.Description as Ded_Desc
from TSPL_PAYMENT_PROCESS_COMPULSORY 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No 
left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction
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
                Dim Qry1 As String = "    Select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,
				                    TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,'' as Gender,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,
									TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No,
									 COALESCE(TSPL_DEDUCTION_MASTER.Code, TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode) AS DCS_Addition_Deduction,
									 COALESCE(TSPL_DEDUCTION_MASTER.Description, TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc)  as DCSDescription,
                                    TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,TSPL_VENDOR_INVOICE_DETAIL.Amount as VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName
									from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
									left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
									left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No 
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
									left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction
									left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
									left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
									left join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
									left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code


                                    Union all	
									 
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
									TSPL_PAYMENT_PROCESS_SAVING.Doc_No,TSPL_DEDUCTION_MASTER.Code as DCS_Addition_Deduction,TSPL_DEDUCTION_MASTER.Description as DCSDescription,
                                    TSPL_VENDOR_INVOICE_DETAIL.Amount as Amount,0 AS VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date ,TSPL_PAYMENT_PROCESS_HEAD.To_Date,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName
									FROM TSPL_PAYMENT_PROCESS_SAVING
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_SAVING.Doc_No
									left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
								    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction
								    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
									left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
									left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code

                                    union all

									SELECT TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,'' as Gender,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,
									TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No,TSPL_DEDUCTION_MASTER.Code as DCS_Addition_Deduction,TSPL_DEDUCTION_MASTER.Description as DCSDescription,
                                    TSPL_VENDOR_INVOICE_DETAIL.Amount as Amount,0 AS VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Document_No as InvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date ,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName
									FROM TSPL_PAYMENT_PROCESS_COMPULSORY
									left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No
									left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
								    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_DCS_ADDITION_DEDUCTION.Deduction 
								    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
									left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
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
	                                left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
                                    union all
									
                                    Select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_VLC_MASTER_HEAD.Registered_PDCS_CLUSTER,TSPL_VENDOR_MASTER.Gender,VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,VSP_NAME,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,
                                   '' as DCS_Addition_Deduction,'' as DCSDescription,
                                    0 as Amount,0 AS VendorAmt,'' as InvoiceNo,'' as Main_VSP_Milk_AP_Invoice_No ,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName
								   from TSPL_PAYMENT_PROCESS_DETAIL
                                   left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                   left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
								   left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
                                   left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                                   left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code"


                Dim sQuery As String = ""
                If rdbSummary.IsChecked AndAlso (rdbDCS.IsChecked OrElse rbtnAliasNameWise.IsChecked) Then
                    sQuery = "  Select "
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " AliasName,"
                    Else
                        sQuery += " VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,"
                    End If
                    sQuery += "max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                                    from 
                                   (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from (Select max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,"
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += "AliasName,"
                    Else
                        sQuery += "VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,"
                    End If
                    sQuery += "sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " from(Select Registered_PDCS_CLUSTER,Gender,"
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " AliasName,"
                    Else
                        sQuery += " VSP_CODE,DCSCode,VSP_NAME,"
                    End If

                    sQuery += "Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,Credit_Note_Amount, " & DescName2 & " from (Select max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,"
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " yy.AliasName,"
                    Else
                        sQuery += "MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,"
                    End If

                    sQuery += " SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount 
                                   from (   Select max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender "
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName "
                    Else
                        sQuery += " ,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME"
                    End If
                    sQuery += " ,Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date 
                                   from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and Vendor_CODE In (" & clsCommon.GetMulcallString(txtDCS.arrValueMember) & ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And MCC in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And Area_Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " group by xx.Doc_No,"
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " xx.AliasName,"
                    Else
                        sQuery += " xx.Vendor_CODE,"
                    End If
                    sQuery += " xx.DCS_Addition_Deduction )YY group by "
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " AliasName,"
                    Else
                        sQuery += " VSP_CODE,"
                    End If

                    sQuery += "DCS_Addition_Deduction)Tab1 
                        PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by "
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " AliasName "
                    Else
                        sQuery += " VSP_CODE "
                    End If
                    sQuery += ")YY 

                        Union all
									Select '' as Registered_PDCS_CLUSTER,'' as Gender,"
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " AliasName,"
                    Else
                        sQuery += " VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,"
                    End If
                    sQuery += "0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                                    0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & "  ,																
									Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty
                                    from (SELECT XX.DOC_CODE,"
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " Max(xx.AliasName)AliasName,"
                    Else
                        sQuery += " max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,"
                    End If
                    sQuery += "SUM(XX.Qty)Qty,XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                                    CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty 
                                    FROM (Select TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                                    TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName 
                                    from TSPL_MILK_PURCHASE_INVOICE_DETAIL
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
                                    left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                   left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                                   left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
                                    left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                                    LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No  
                                    where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                                    and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103) "
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_MCC_MASTER.MCC_Code in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " )XX GROUP BY DOC_CODE,QBD)yy group by "
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " AliasName "
                    Else
                        sQuery += " VSP_CODE "
                    End If

                    sQuery += " )Tab2 group by "
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " AliasName "
                    Else
                        sQuery += " VSP_CODE "
                    End If
                    If rbtnAliasNameWise.IsChecked Then
                        sQuery += " order by AliasName "
                    Else
                        sQuery += " order by cast(max(DCSCode)  as int) "
                    End If
                    'sQuery += " order by cast(max(DCSCode)  as int) "

                ElseIf rdbSummary.IsChecked AndAlso rdbBMC.IsChecked = True Then
                    sQuery = "  Select MCC,max(MCC_NAME)MCC_NAME,MAX(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                                    from 
                                   (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from (Select MAX(MCC)MCC,MAX(MCC_NAME)MCC_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " 
                                    from(Select MCC,MCC_NAME,Registered_PDCS_CLUSTER,Gender,VSP_CODE,DCSCode,VSP_NAME,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " 
                                     from (Select MAX(MCC)MCC,MAX(MCC_NAME)MCC_NAME,max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount 
                                   from (   Select MAX(MCC)MCC,MAX(MCC_NAME)MCC_NAME,max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME,Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date 
                                   from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and Vendor_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And MCC in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And Area_Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " group by xx.Doc_No,xx.Vendor_CODE,xx.DCS_Addition_Deduction )YY group by VSP_CODE,DCS_Addition_Deduction)Tab1 
                        PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by VSP_CODE )YY 

                        Union all
						Select  MAX(MCC)MCC,MAX(MCC_NAME)MCC_NAME,'' as Registered_PDCS_CLUSTER,'' as Gender,VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                        0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & "  ,																
						Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty
                        from (SELECT MAX(XX.MCC)MCC,MAX(MCC_NAME)MCC_NAME,XX.DOC_CODE,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,SUM(XX.Qty)Qty,
                        XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                        CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty 
                        FROM (Select TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                        TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD from TSPL_MILK_PURCHASE_INVOICE_DETAIL
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                        left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                        left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                        left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
                        left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                        LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No  
                        where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                        and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103) "
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_MCC_MASTER.MCC_Code in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " )XX GROUP BY DOC_CODE,QBD)yy group by VSP_CODE )Tab2 group by MCC order by MCC  "

                ElseIf rdbSummary.IsChecked AndAlso rdbBMCDCS.IsChecked = True Then
                    sQuery = "    WITH BaseData AS ( Select MCC,max(MCC_NAME)MCC_NAME,MAX(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                                    from 
                                   (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from (Select MAX(MCC)MCC,MAX(MCC_NAME)MCC_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " 
                                    from(Select MCC,MCC_NAME,Registered_PDCS_CLUSTER,Gender,VSP_CODE,DCSCode,VSP_NAME,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " 
                                     from (Select MAX(MCC)MCC,MAX(MCC_NAME)MCC_NAME,max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount 
                                   from (   Select MAX(MCC)MCC,MAX(MCC_NAME)MCC_NAME,max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME,Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date 
                                   from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and Vendor_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And MCC in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And Area_Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " group by xx.Doc_No,xx.Vendor_CODE,xx.DCS_Addition_Deduction )YY group by VSP_CODE,DCS_Addition_Deduction)Tab1 
                        PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by VSP_CODE )YY 

                        Union all
									Select  MAX(MCC)MCC,MAX(MCC_NAME)MCC_NAME,'' as Registered_PDCS_CLUSTER,'' as Gender,VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                                    0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & "  ,																
									Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty
                                    from (SELECT MAX(XX.MCC)MCC,MAX(MCC_NAME)MCC_NAME,XX.DOC_CODE,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,SUM(XX.Qty)Qty,
                                    XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                                    CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty 
                                    FROM (Select TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                                    TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD from TSPL_MILK_PURCHASE_INVOICE_DETAIL
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                                    left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                                    left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
                                    left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                                    LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No  
                                    where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                                    and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103) "
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_MCC_MASTER.MCC_Code in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " )XX GROUP BY DOC_CODE,QBD)yy group by VSP_CODE )Tab2 group by MCC,VSP_CODE)
SELECT * FROM BaseData
Union all
SELECT MCC as MCC,max(MCC_NAME) as MCC_NAME,'' AS VSP_CODE,'Total of ' + max(MCC_NAME) AS DCSCode,'' AS VSP_NAME,'' AS Registered_PDCS_CLUSTER,
    '' AS Gender,SUM(Milk_Qty) AS Milk_Qty,SUM(Milk_Amount) AS Milk_Amount,SUM(Head_Load_Amount) AS Head_Load_Amount,SUM(Deduction_Amount) AS Deduction_Amount,
    SUM(Credit_Note_Amount) AS Credit_Note_Amount," & DescName4 & ",SUM(SweetQty) AS SweetQty,SUM(SourQty) AS SourQty,SUM(CurdQty) AS CurdQty,SUM(Payable_Amount) AS Payable_Amount
FROM BaseData GROUP BY MCC order by MCC,DCSCode "
                ElseIf rdbSummary.IsChecked AndAlso rdbArea.IsChecked Then
                    sQuery = "  Select Area_Location_Code,MAX(Location_Desc)Location_Desc,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                                    from 
                                   (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from (Select Area_Location_Code,MAX(Location_Desc)Location_Desc,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " 
                                    from(Select Area_Location_Code,Location_Desc,Registered_PDCS_CLUSTER,Gender,VSP_CODE,DCSCode,VSP_NAME,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " 
                                     from (Select Area_Location_Code,MAX(Location_Desc)Location_Desc,max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount 
                                   from (   Select Area_Location_Code,MAX(Location_Desc)Location_Desc,max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME,Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date 
                                   from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and Vendor_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And MCC in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And Area_Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " group by xx.Doc_No,Area_Location_Code,xx.Vendor_CODE,xx.DCS_Addition_Deduction )YY group by Area_Location_Code,DCS_Addition_Deduction)Tab1 
                        PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by Area_Location_Code )YY 

                        Union all
						Select  Area_Location_Code,Max(Location_Desc)Location_Desc,'' as Registered_PDCS_CLUSTER,'' as Gender,Max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                        0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & "  ,																
						Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty
                        from (SELECT Area_Location_Code,MAX(Location_Desc)Location_Desc,XX.DOC_CODE,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,SUM(XX.Qty)Qty,
                        XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                        CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty 
                        FROM (Select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                        TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD from TSPL_MILK_PURCHASE_INVOICE_DETAIL
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                        left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                        left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                        left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
                        left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                        LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No  
                        where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                        and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103) "
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_MCC_MASTER.MCC_Code in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " )XX GROUP BY DOC_CODE,QBD,Area_Location_Code)yy group by Area_Location_Code )Tab2 group by Area_Location_Code order by Area_Location_Code  "

                ElseIf rdbMonth.IsChecked = True Then
                    sQuery = "  Select max(Month_Name)Month_Name,Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,Max(AliasName)AliasName,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                            from (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty 
                                   from (Select Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,Max(AliasName)AliasName,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " ,max(Month_Name)Month_Name,Month_Number
                            from (Select Area_Location_Code,Location_Desc,Registered_PDCS_CLUSTER,Gender,MCC,MCC_NAME,VSP_CODE,DCSCode,VSP_NAME,AliasName,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " ,Month_Name,Month_Number
                                   
								   from (Select max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,Max(yy.Area_Location_Code)Area_Location_Code,MAX(yy.Location_Desc)Location_Desc,Max(yy.MCC)MCC,MAX(yy.MCC_NAME)MCC_NAME,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,Max(AliasName)AliasName,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(Month_Name)Month_Name,Month_Number
                                   
								   from (   Select max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,Max(xx.Area_Location_Code)Area_Location_Code,MAX(xx.Location_Desc)Location_Desc,Max(xx.MCC)MCC,MAX(xx.MCC_NAME)MCC_NAME,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Max(Vendor_CODE) as VSP_CODE,max(Vendor_Name)VSP_NAME,Max(AliasName)AliasName,Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date,
                                   DATENAME(MONTH, MAX(From_Date)) AS Month_Name,MONTH(MAX(From_Date)) AS Month_Number
                                   from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and Vendor_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And MCC in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And Area_Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " group by xx.Doc_No,"
                    If rdbDCS.IsChecked Then
                        sQuery += " xx.Vendor_CODE,"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " xx.MCC,xx.Vendor_CODE,"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " xx.MCC,"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " xx.AliasName,"
                    Else
                        sQuery += " xx.Area_Location_Code,"
                    End If

                    sQuery += " xx.DCS_Addition_Deduction)YY group by Month_Number"
                    If rdbDCS.IsChecked Then
                        sQuery += ",VLC_CODE_Uploader"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " ,MCC,VLC_CODE_Uploader"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " ,MCC"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName"
                    Else
                        sQuery += ",Area_Location_Code"
                    End If
                    sQuery += ",DCS_Addition_Deduction) Tab1 PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by Month_Number"
                    If rdbDCS.IsChecked Then
                        sQuery += ",DCSCode"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " ,MCC,DCSCode"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " ,MCC"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName"
                    Else
                        sQuery += ",Area_Location_Code"
                    End If
                    sQuery += " )YY

                    Union all
                             Select '' as Registered_PDCS_CLUSTER,'' as Gender,Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,Max(AliasName)AliasName,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                             0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & ",max(Month_Name)Month_Name,Month_Number,Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty
                              from (SELECT XX.DOC_CODE,Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,Max(xx.AliasName)AliasName,SUM(xx.Qty)Qty,
                                    XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                                    CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty,max(Month_Name)Month_Name,Month_Number 
                                    FROM (Select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_MILK_SRN_HEAD.MCC_CODE As MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                                    TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD ,
									DATENAME(MONTH, (DOC_DATE)) AS Month_Name,MONTH((DOC_DATE)) AS Month_Number,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName									
									from TSPL_MILK_PURCHASE_INVOICE_DETAIL
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
                                    left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                    left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                                    LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No 
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                                    Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
                                    where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                                    and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103) "
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_MCC_MASTER.MCC_Code in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " )XX GROUP BY DOC_CODE,QBD,Month_Number "
                    If rdbDCS.IsChecked Then
                        sQuery += ",VLC_Code_VLC_Uploader"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " ,MCC,VLC_Code_VLC_Uploader"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " ,MCC"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName"
                    Else
                        sQuery += ",Area_Location_Code"
                    End If
                    sQuery += ")yy group by Month_Number "
                    If rdbDCS.IsChecked Then
                        sQuery += ",DCSCode"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " ,MCC,DCSCode"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " ,MCC"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName"
                    Else
                        sQuery += ",Area_Location_Code"
                    End If
                    sQuery += " )Tab2 "
                    If rdbArea.IsChecked Then
                        sQuery += " where ISNULL(Area_Location_Code,'')<>'' "
                    End If
                    sQuery += " group by Month_Number "
                    If rdbDCS.IsChecked Then
                        sQuery += ",DCSCode"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " ,MCC,DCSCode"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " ,MCC"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName"
                    Else
                        sQuery += ",Area_Location_Code"
                    End If

                ElseIf rdbCycleW.IsChecked = True Then
                    If rdbBMC.IsChecked OrElse rdbArea.IsChecked Then
                        sQuery = "With BaseData As ("
                    End If

                    sQuery += "Select FORMAT(MAX(From_Date), 'dd-MM') + ' to ' + FORMAT(To_Date, 'dd-MM') AS Date_Range
                           ,To_Date,max(From_Date)From_Date,Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,Max(AliasName)AliasName,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                                    from 
                                   (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from 
                                   (Select Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,Max(AliasName)AliasName,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " ,To_Date ,max(From_Date)From_Date 
                                   from(Select Area_Location_Code,Location_Desc,MCC,MCC_NAME,Registered_PDCS_CLUSTER,Gender,VSP_CODE,DCSCode,VSP_NAME,AliasName,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " ,From_Date,To_Date 
                                   from (Select Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,Max(yy.AliasName)AliasName,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,max(From_Date)From_Date ,(To_Date)To_Date
                                   from (   Select Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Max(Vendor_CODE) as VSP_CODE,max(Vendor_Name)VSP_NAME,Max(AliasName)AliasName,
                                   Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date ,
                                   max(To_Date)To_Date from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and Vendor_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And MCC in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And Area_Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " group by xx.Doc_No,"
                    If rdbDCS.IsChecked Then
                        sQuery += " xx.Vendor_CODE,"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " xx.MCC,xx.Vendor_CODE,"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " xx.MCC,"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " xx.AliasName,"
                    Else
                        sQuery += " xx.Area_Location_Code,"
                    End If
                    sQuery += " xx.DCS_Addition_Deduction )YY group by To_Date,"
                    If rdbDCS.IsChecked Then
                        sQuery += " VSP_CODE,"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " MCC,VSP_CODE,"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " MCC,"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " AliasName,"
                    Else
                        sQuery += " Area_Location_Code,"
                    End If
                    sQuery += " DCS_Addition_Deduction)Tab1 PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by To_Date "
                    If rdbDCS.IsChecked Then
                        sQuery += ",DCSCode"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " ,MCC,DCSCode"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " ,MCC"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName"
                    Else
                        sQuery += ",Area_Location_Code"
                    End If
                    sQuery += " )YY 
                        
                            Union all
						    Select '' as Registered_PDCS_CLUSTER,'' as Gender,Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,MAX(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,max(AliasName)AliasName,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                                    0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & ",DOC_DATE,'' as From_Date,																
									Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty FROM (SELECT XX.DOC_CODE,Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,Max(xx.AliasName)AliasName,SUM(XX.Qty)Qty,
                                    XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                                    CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty,MAX(DOC_DATE)DOC_DATE 
                                    FROM (Select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName,
                                    TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE
									from TSPL_MILK_PURCHASE_INVOICE_DETAIL
									LEFT OUTER JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD ON TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
                                    left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                    left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                                    LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                                    Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
                                    where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                                    and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103)"
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_MCC_MASTER.MCC_Code in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " )XX GROUP BY DOC_CODE,QBD ) XX GROUP BY DOC_DATE)Tab2 "
                    If rdbArea.IsChecked Then
                        sQuery += " where ISNULL(Area_Location_Code,'')<>'' "
                    End If
                    sQuery += " group by To_Date "
                    If rdbDCS.IsChecked Then
                        sQuery += ",DCSCode"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " ,MCC,DCSCode"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " ,MCC"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName"
                    Else
                        sQuery += ",Area_Location_Code"
                    End If
                    If rdbBMC.IsChecked Then
                        sQuery += " ) Select * from BaseData "
                        sQuery += " Union All "
                        sQuery += " Select 'Total of '+ Max(MCC_NAME) As Date_Range,MAX(To_Date) As To_Date,MIN(From_Date) As From_Date, Null As Area_Location_Code, Null As Location_Desc,MCC As MCC,Max(MCC_Name) As MCC_Name,Null As VSP_CODE,Null As DCSCode, Null VSP_NAME,Null As AliasName,Null As Registered_PDCS_CLUSTER,Null As Gender, SUM(Milk_Qty)Milk_Qty,SUM(Milk_Amount)Milk_Amount,SUM(Head_Load_Amount)Head_Load_Amount,SUM(Deduction_Amount)Deduction_Amount,SUM(Credit_Note_Amount)Credit_Note_Amount," & DescName4 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount from BaseData Group By MCC Order By MCC ,To_Date "
                    ElseIf rdbArea.IsChecked Then
                        sQuery += " ) Select * from BaseData "
                        sQuery += " Union All "
                        sQuery += " Select 'Total of '+ Max(Location_Desc) As Date_Range,MAX(To_Date) As To_Date,MIN(From_Date) As From_Date, Max(Area_Location_Code) As Area_Location_Code, Max(Location_Desc) As Location_Desc,Null As MCC,Null As MCC_Name,Null As VSP_CODE,Null As DCSCode, Null VSP_NAME,Null As AliasName,Null As Registered_PDCS_CLUSTER,Null As Gender, SUM(Milk_Qty)Milk_Qty,SUM(Milk_Amount)Milk_Amount,SUM(Head_Load_Amount)Head_Load_Amount,SUM(Deduction_Amount)Deduction_Amount,SUM(Credit_Note_Amount)Credit_Note_Amount," & DescName4 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount from BaseData Group By Area_Location_Code Order By Area_Location_Code ,To_Date "
                    End If
                ElseIf rdbMonthCycle.IsChecked = True Then
                    sQuery = "  With BaseData As ( Select DATENAME(MONTH, max(From_Date)) As Month_Name,MONTH(max(From_Date)) As Month_Number,FORMAT(MAX(From_Date), 'dd-MM') + ' to ' + FORMAT(To_Date, 'dd-MM') AS Date_Range
                           ,To_Date,max(From_Date)From_Date,Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,Max(AliasName)AliasName,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,sum(Milk_Qty)Milk_Qty,
                                    sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Deduction_Amount)Deduction_Amount,
                                    sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName3 & ",Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty,sum(Payable_Amount)Payable_Amount
                                    from 
                                   (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from 
                                   (Select Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,max(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,Max(AliasName)AliasName,sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,
                                   sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,
                                   sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " ,To_Date ,max(From_Date)From_Date 
                                   from(Select Area_Location_Code,Location_Desc,MCC,MCC_NAME,Registered_PDCS_CLUSTER,Gender,VSP_CODE,DCSCode,VSP_NAME,AliasName,Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,
                                   Credit_Note_Amount, " & DescName2 & " ,From_Date,To_Date 
                                   from (Select Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,Max(yy.AliasName)AliasName,SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,max(From_Date)From_Date ,(To_Date)To_Date
                                   from (   Select Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME,Max(AliasName)AliasName,
                                   Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date ,
                                   max(To_Date)To_Date from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and Vendor_CODE In (" & clsCommon.GetMulcallString(txtDCS.arrValueMember) & ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And MCC in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And Area_Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " group by xx.Doc_No,xx.Vendor_CODE,xx.DCS_Addition_Deduction )YY group by To_Date,"
                    If rdbDCS.IsChecked Then
                        sQuery += " VSP_CODE,"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " MCC,VSP_CODE,"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " MCC,"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " AliasName,"
                    Else
                        sQuery += " Area_Location_Code,"
                    End If
                    sQuery += " DCS_Addition_Deduction)Tab1  PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by To_Date "
                    If rdbDCS.IsChecked Then
                        sQuery += ",DCSCode"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " ,MCC,DCSCode"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " ,MCC"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName"
                    Else
                        sQuery += ",Area_Location_Code"
                    End If
                    sQuery += " )YY 
                            Union all
						    Select '' as Registered_PDCS_CLUSTER,'' as Gender,Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,MAX(VSP_CODE)VSP_CODE,max(DCSCode)DCSCode,max(VLC_Name)VLC_Name,Max(AliasName)AliasName,0 as Milk_Qty,0 as Milk_Amount,0 as Head_Load_Amount,0 as Payable_Amount,
                                    0 as Deduction_Amount,0 as Credit_Note_Amount," & DescName & ",DOC_DATE,'' as From_Date,																
									Sum(SweetQty)SweetQty,Sum(SourQty)SourQty,sum(CurdQty)CurdQty FROM (SELECT XX.DOC_CODE,Max(Area_Location_Code)Area_Location_Code,MAX(Location_Desc)Location_Desc,Max(MCC)MCC,Max(MCC_NAME)MCC_NAME,max(xx.VSP_CODE)VSP_CODE,max(xx.VLC_Code_VLC_Uploader)DCSCode,max(xx.VLC_Name)VLC_Name,Max(xx.AliasName)AliasName,SUM(XX.Qty)Qty,
                                    XX.QBD,CASE WHEN QBD='SWEET' THEN sum(isnull(Qty,0)) end AS SweetQty,CASE WHEN QBD='SOUR' THEN sum(isnull(Qty,0)) end AS SourQty,
                                    CASE WHEN QBD='CURD' THEN sum(isnull(Qty,0)) end AS CurdQty,MAX(DOC_DATE)DOC_DATE 
                                    FROM (Select TSPL_MCC_MASTER.Area_Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
                                    TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE,Qty,(case when  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE
									,Case When ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'')<>'' Then TSPL_VENDOR_MASTER.Alies_Name Else TSPL_VENDOR_MASTER.Vendor_Name End As AliasName
									from TSPL_MILK_PURCHASE_INVOICE_DETAIL
									LEFT OUTER JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD ON TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO
                                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
                                    left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                    left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                                    LEFT OUTER JOIN TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                                    Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Area_Location_Code
                                    where 2=2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)>=convert(date,'" & fromDate.Value & "',103) 
                                    and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)<=convert(date,'" & ToDate.Value & "',103)"
                    If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE In (" & clsCommon.GetMulcallString(txtDCS.arrValueMember) & ") "
                    End If
                    If txtMultBMC.arrValueMember IsNot Nothing AndAlso txtMultBMC.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_MCC_MASTER.MCC_Code in (" & clsCommon.GetMulcallString(txtMultBMC.arrValueMember) & ")"
                    End If
                    If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                        sQuery += " And TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
                    End If
                    sQuery += " )XX GROUP BY DOC_CODE,QBD ) XX GROUP BY DOC_DATE)Tab2"
                    If rdbArea.IsChecked Then
                        sQuery += " where ISNULL(Area_Location_Code,'')<>'' "
                    End If
                    sQuery += " group by To_Date "
                    If rdbDCS.IsChecked Then
                        sQuery += ",DCSCode"
                    ElseIf rdbBMCDCS.IsChecked Then
                        sQuery += " ,MCC,DCSCode"
                    ElseIf rdbBMC.IsChecked Then
                        sQuery += " ,MCC"
                    ElseIf rbtnAliasNameWise.IsChecked Then
                        sQuery += " ,AliasName"
                    Else
                        sQuery += ",Area_Location_Code"
                    End If
                    sQuery += ") SELECT * FROM BaseData
Union all
SELECT 
    NULL AS Month_Name,Month_Number,'Total of ' + DATENAME(MONTH, DATEFROMPARTS(YEAR(max(To_Date)), Month_Number, 1)) AS Date_Range,
    NULL AS To_Date,NULL AS From_Date,Null As Area_Location_Code,Null As Location_Desc,Null As MCC,Null As MCC_NAME,Null  AS VSP_CODE,Null  AS DCSCode,Null As VSP_NAME,Null As AliasName,Null As Registered_PDCS_CLUSTER,
    Null As Gender,SUM(Milk_Qty) AS Milk_Qty,SUM(Milk_Amount) AS Milk_Amount,SUM(Head_Load_Amount) AS Head_Load_Amount,SUM(Deduction_Amount) AS Deduction_Amount,
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
                    If rdbBMC.IsChecked Then
                        Dim dtNew As DataTable = dt.Clone()
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim distinctMCCs = (From row In dt.AsEnumerable()
                                                Select New With {Key .MCCCode = row.Field(Of String)("MCC"),
                                                                 Key .MCCName = row.Field(Of String)("MCC_Name")}).Distinct().ToList()

                            For Each MCC In distinctMCCs
                                dtNew.Rows.Add(MCC.MCCName)
                                Dim originalRows = dt.AsEnumerable().Where(Function(r) r.Field(Of String)("MCC") = MCC.MCCCode)
                                For Each row As DataRow In originalRows
                                    dtNew.ImportRow(row)
                                Next
                            Next
                        End If
                        gv1.DataSource = dtNew
                    ElseIf rdbArea.IsChecked Then
                        Dim dtNew As DataTable = dt.Clone()
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim distinctAreas = (From row In dt.AsEnumerable()
                                                 Select New With {Key .AreaCode = row.Field(Of String)("Area_Location_Code"),
                                                                 Key .AreaName = row.Field(Of String)("Location_Desc")}).Distinct().ToList()

                            For Each Areas In distinctAreas
                                dtNew.Rows.Add(Areas.AreaName)
                                Dim originalRows = dt.AsEnumerable().Where(Function(r) r.Field(Of String)("Area_Location_Code") = Areas.AreaCode)
                                For Each row As DataRow In originalRows
                                    dtNew.ImportRow(row)
                                Next
                            Next
                        End If
                        gv1.DataSource = dtNew
                    Else
                        gv1.DataSource = dt
                    End If

                    gv1.BestFitColumns()
                    SetGridFormation()
                    RadPageView2.SelectedPage = RadPageViewPage5
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
        Dim index As Integer
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rdbSummary.IsChecked AndAlso (rdbDCS.IsChecked OrElse rbtnAliasNameWise.IsChecked) Then
            If rbtnAliasNameWise.IsChecked Then
                gv1.Columns("AliasName").HeaderText = "Alias Name"
            Else
                gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
                gv1.Columns("DCSCode").HeaderText = "DCS Code"
                gv1.Columns("VSP_NAME").HeaderText = "DCS NAME"
            End If
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

            index = 5
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next

        ElseIf rdbSummary.IsChecked = True AndAlso rdbBMC.IsChecked = True Then
            gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
            gv1.Columns("VSP_CODE").IsVisible = False
            gv1.Columns("VSP_CODE").VisibleInColumnChooser = True

            gv1.Columns("MCC").HeaderText = "MCC"
            gv1.Columns("MCC").IsVisible = True
            gv1.Columns("MCC").VisibleInColumnChooser = True

            gv1.Columns("DCSCode").HeaderText = "DCS Code"
            gv1.Columns("DCSCode").IsVisible = False
            gv1.Columns("DCSCode").VisibleInColumnChooser = True

            gv1.Columns("VSP_NAME").HeaderText = "DCS Name"
            gv1.Columns("VSP_NAME").IsVisible = False
            gv1.Columns("VSP_NAME").VisibleInColumnChooser = True

            gv1.Columns("Registered_PDCS_CLUSTER").HeaderText = "Registered_PDCS_CLUSTER"
            gv1.Columns("Registered_PDCS_CLUSTER").IsVisible = False
            gv1.Columns("Registered_PDCS_CLUSTER").VisibleInColumnChooser = True

            gv1.Columns("Gender").HeaderText = "Gender"
            gv1.Columns("Gender").IsVisible = False
            gv1.Columns("Gender").VisibleInColumnChooser = True

            gv1.Columns("MCC_NAME").HeaderText = "MCC NAME"
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

            index = 2
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbSummary.IsChecked = True AndAlso rdbArea.IsChecked = True Then
            gv1.Columns("Area_Location_Code").HeaderText = "Area Code"
            gv1.Columns("Area_Location_Code").IsVisible = False
            gv1.Columns("Area_Location_Code").VisibleInColumnChooser = True

            gv1.Columns("Location_Desc").HeaderText = "Area Name"
            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").VisibleInColumnChooser = True

            gv1.Columns("Registered_PDCS_CLUSTER").HeaderText = "Registered_PDCS_CLUSTER"
            gv1.Columns("Registered_PDCS_CLUSTER").IsVisible = False
            gv1.Columns("Registered_PDCS_CLUSTER").VisibleInColumnChooser = True

            gv1.Columns("Gender").HeaderText = "Gender"
            gv1.Columns("Gender").IsVisible = False
            gv1.Columns("Gender").VisibleInColumnChooser = True

            'gv1.Columns("MCC_NAME").HeaderText = "MCC NAME"
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

            index = 2
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbSummary.IsChecked = True AndAlso rdbBMCDCS.IsChecked = True Then
            gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
            gv1.Columns("VSP_CODE").IsVisible = False
            gv1.Columns("VSP_CODE").VisibleInColumnChooser = True

            gv1.Columns("MCC").HeaderText = "MCC"
            gv1.Columns("MCC").IsVisible = True
            gv1.Columns("MCC").VisibleInColumnChooser = True

            gv1.Columns("MCC_NAME").HeaderText = "MCC NAME"
            gv1.Columns("MCC_NAME").IsVisible = True
            gv1.Columns("MCC_NAME").VisibleInColumnChooser = True

            gv1.Columns("DCSCode").HeaderText = "DCS Code"
            gv1.Columns("DCSCode").IsVisible = True
            gv1.Columns("DCSCode").VisibleInColumnChooser = True

            gv1.Columns("VSP_NAME").HeaderText = "DCS Name"
            gv1.Columns("VSP_NAME").IsVisible = True
            gv1.Columns("VSP_NAME").VisibleInColumnChooser = True

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

            index = 7
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbMonth.IsChecked = True Then
            If rdbDCS.IsChecked Then
                gv1.Columns("DCSCode").HeaderText = "DCS Code"
                gv1.Columns("DCSCode").IsVisible = True
                gv1.Columns("DCSCode").VisibleInColumnChooser = True
                gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
                gv1.Columns("VSP_CODE").IsVisible = True
                gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
                gv1.Columns("VSP_NAME").HeaderText = "VSP Name"
                gv1.Columns("VSP_NAME").IsVisible = True
                gv1.Columns("VSP_NAME").VisibleInColumnChooser = True
                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC_Name").IsVisible = False
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            ElseIf rdbBMCDCS.IsChecked Then
                gv1.Columns("MCC").HeaderText = "MCC"
                gv1.Columns("MCC").IsVisible = True
                gv1.Columns("MCC").VisibleInColumnChooser = True
                gv1.Columns("MCC_Name").HeaderText = "MCC Name"
                gv1.Columns("MCC_Name").IsVisible = True
                gv1.Columns("MCC_Name").VisibleInColumnChooser = True
                gv1.Columns("DCSCode").HeaderText = "DCS Code"
                gv1.Columns("DCSCode").IsVisible = True
                gv1.Columns("DCSCode").VisibleInColumnChooser = True
                gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
                gv1.Columns("VSP_CODE").IsVisible = True
                gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
                gv1.Columns("VSP_NAME").HeaderText = "VSP Name"
                gv1.Columns("VSP_NAME").IsVisible = True
                gv1.Columns("VSP_NAME").VisibleInColumnChooser = True
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            ElseIf rdbBMC.IsChecked Then
                gv1.Columns("MCC").HeaderText = "MCC"
                gv1.Columns("MCC").IsVisible = True
                gv1.Columns("MCC").VisibleInColumnChooser = True
                gv1.Columns("MCC_Name").HeaderText = "MCC Name"
                gv1.Columns("MCC_Name").IsVisible = True
                gv1.Columns("MCC_Name").VisibleInColumnChooser = True
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_NAME").IsVisible = False
                gv1.Columns("DCSCode").IsVisible = False
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            ElseIf rbtnAliasNameWise.IsChecked Then
                gv1.Columns("AliasName").HeaderText = "Alias Name"
                gv1.Columns("AliasName").IsVisible = True
                gv1.Columns("DCSCode").IsVisible = False
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_NAME").IsVisible = False
                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC_Name").IsVisible = False
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
            Else
                gv1.Columns("Area_Location_Code").HeaderText = "Area Code"
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Area_Location_Code").VisibleInColumnChooser = True
                gv1.Columns("Location_Desc").HeaderText = "Area Name"
                gv1.Columns("Location_Desc").IsVisible = True
                gv1.Columns("Location_Desc").VisibleInColumnChooser = True
                gv1.Columns("DCSCode").IsVisible = False
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_NAME").IsVisible = False
                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC_Name").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            End If
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
            index = 8
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbCycleW.IsChecked = True Then
            If rdbDCS.IsChecked Then
                gv1.Columns("DCSCode").HeaderText = "DCS Code"
                gv1.Columns("DCSCode").IsVisible = True
                gv1.Columns("DCSCode").VisibleInColumnChooser = True
                gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
                gv1.Columns("VSP_CODE").IsVisible = True
                gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
                gv1.Columns("VSP_NAME").HeaderText = "VSP Name"
                gv1.Columns("VSP_NAME").IsVisible = True
                gv1.Columns("VSP_NAME").VisibleInColumnChooser = True
                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC_Name").IsVisible = False
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            ElseIf rdbBMCDCS.IsChecked Then
                gv1.Columns("MCC").HeaderText = "MCC"
                gv1.Columns("MCC").IsVisible = True
                gv1.Columns("MCC").VisibleInColumnChooser = True
                gv1.Columns("MCC_Name").HeaderText = "MCC Name"
                gv1.Columns("MCC_Name").IsVisible = True
                gv1.Columns("MCC_Name").VisibleInColumnChooser = True
                gv1.Columns("DCSCode").HeaderText = "DCS Code"
                gv1.Columns("DCSCode").IsVisible = True
                gv1.Columns("DCSCode").VisibleInColumnChooser = True
                gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
                gv1.Columns("VSP_CODE").IsVisible = True
                gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
                gv1.Columns("VSP_NAME").HeaderText = "VSP Name"
                gv1.Columns("VSP_NAME").IsVisible = True
                gv1.Columns("VSP_NAME").VisibleInColumnChooser = True
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            ElseIf rdbBMC.IsChecked Then
                gv1.Columns("MCC").HeaderText = "MCC"
                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC").VisibleInColumnChooser = True
                gv1.Columns("MCC_Name").HeaderText = "MCC Name"
                gv1.Columns("MCC_Name").IsVisible = False
                gv1.Columns("MCC_Name").VisibleInColumnChooser = True
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_NAME").IsVisible = False
                gv1.Columns("DCSCode").IsVisible = False
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            ElseIf rbtnAliasNameWise.IsChecked Then
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("DCSCode").IsVisible = False
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_NAME").IsVisible = False
                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC_Name").IsVisible = False

                gv1.Columns("AliasName").HeaderText = "Alias Name"
                gv1.Columns("AliasName").IsVisible = True
            Else
                gv1.Columns("Area_Location_Code").HeaderText = "Area Code"
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Area_Location_Code").VisibleInColumnChooser = True
                gv1.Columns("Location_Desc").HeaderText = "Area Name"
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("Location_Desc").VisibleInColumnChooser = True
                gv1.Columns("AliasName").IsVisible = False
                gv1.Columns("DCSCode").IsVisible = False
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_NAME").IsVisible = False
                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC_Name").IsVisible = False

            End If

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
            If rbtnAliasNameWise.IsChecked Then
                index = 11
            Else
                index = 10
            End If
            For ii As Integer = index To gv1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbMonthCycle.IsChecked Then
            If rdbDCS.IsChecked Then
                gv1.Columns("DCSCode").HeaderText = "DCS Code"
                gv1.Columns("DCSCode").IsVisible = True
                gv1.Columns("DCSCode").VisibleInColumnChooser = True
                gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
                gv1.Columns("VSP_CODE").IsVisible = True
                gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
                gv1.Columns("VSP_NAME").HeaderText = "VSP Name"
                gv1.Columns("VSP_NAME").IsVisible = True
                gv1.Columns("VSP_NAME").VisibleInColumnChooser = True
                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC_Name").IsVisible = False
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            ElseIf rdbBMCDCS.IsChecked Then
                gv1.Columns("MCC").HeaderText = "MCC"
                gv1.Columns("MCC").IsVisible = True
                gv1.Columns("MCC").VisibleInColumnChooser = True
                gv1.Columns("MCC_Name").HeaderText = "MCC Name"
                gv1.Columns("MCC_Name").IsVisible = True
                gv1.Columns("MCC_Name").VisibleInColumnChooser = True
                gv1.Columns("DCSCode").HeaderText = "DCS Code"
                gv1.Columns("DCSCode").IsVisible = True
                gv1.Columns("DCSCode").VisibleInColumnChooser = True
                gv1.Columns("VSP_CODE").HeaderText = "VSP Code"
                gv1.Columns("VSP_CODE").IsVisible = True
                gv1.Columns("VSP_CODE").VisibleInColumnChooser = True
                gv1.Columns("VSP_NAME").HeaderText = "VSP Name"
                gv1.Columns("VSP_NAME").IsVisible = True
                gv1.Columns("VSP_NAME").VisibleInColumnChooser = True
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            ElseIf rdbBMC.IsChecked Then
                gv1.Columns("MCC").HeaderText = "MCC"
                gv1.Columns("MCC").IsVisible = True
                gv1.Columns("MCC").VisibleInColumnChooser = True
                gv1.Columns("MCC_Name").HeaderText = "MCC Name"
                gv1.Columns("MCC_Name").IsVisible = True
                gv1.Columns("MCC_Name").VisibleInColumnChooser = True
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_NAME").IsVisible = False
                gv1.Columns("DCSCode").IsVisible = False
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
                gv1.Columns("AliasName").IsVisible = False
            ElseIf rbtnAliasNameWise.IsChecked Then
                gv1.Columns("AliasName").HeaderText = "Alias Name"
                gv1.Columns("AliasName").IsVisible = True

                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC_Name").IsVisible = False
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_NAME").IsVisible = False
                gv1.Columns("DCSCode").IsVisible = False
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Location_Desc").IsVisible = False
            Else
                gv1.Columns("Area_Location_Code").HeaderText = "Area Code"
                gv1.Columns("Area_Location_Code").IsVisible = False
                gv1.Columns("Area_Location_Code").VisibleInColumnChooser = True
                gv1.Columns("Location_Desc").HeaderText = "Area Name"
                gv1.Columns("Location_Desc").IsVisible = True
                gv1.Columns("Location_Desc").VisibleInColumnChooser = True
                gv1.Columns("AliasName").IsVisible = False
                gv1.Columns("DCSCode").IsVisible = False
                gv1.Columns("VSP_CODE").IsVisible = False
                gv1.Columns("VSP_NAME").IsVisible = False
                gv1.Columns("MCC").IsVisible = False
                gv1.Columns("MCC_Name").IsVisible = False

            End If
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
        RadPageView2.SelectedPage = RadPageViewPage4
        If AreaWiseBilling Then
            rdbArea.Visible = True
        Else
            rdbArea.Visible = False
        End If
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        txtDCS.Enabled = val
        RadGroupBox11.Enabled = val
        RadGroupBox6.Enabled = val
        RadGroupBox1.Enabled = val
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

    Private Sub rdbMonth_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbMonth.ToggleStateChanged
        'RadGroupBox1.Enabled = False
        RadGroupBox1.Enabled = True
    End Sub

    Private Sub rdbSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbSummary.ToggleStateChanged
        RadGroupBox1.Enabled = True
    End Sub

    Private Sub rdbCycleW_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbCycleW.ToggleStateChanged
        'RadGroupBox1.Enabled = False
        RadGroupBox1.Enabled = True
    End Sub

    Private Sub rdbMonthCycle_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbMonthCycle.ToggleStateChanged
        'RadGroupBox1.Enabled = False
        RadGroupBox1.Enabled = True
    End Sub

    Private Sub txtMultArea__My_Click(sender As Object, e As EventArgs) Handles txtMultArea._My_Click
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER Where TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc' "
            txtMultArea.arrValueMember = clsCommon.ShowMultipleSelectForm("@Area", sQuery, "Code", "Code", txtMultArea.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultBMC__My_Click(sender As Object, e As EventArgs) Handles txtMultBMC._My_Click
        Try
            Dim arrMCCRights As ArrayList
            arrMCCRights = clsMCCCodes.GetUserHavingMCCRights()

            Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as UploaderNo,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where tspl_mcc_master.mcc_Code in (" & StrPermission & ") and (  tspl_mcc_master.mcc_Code in (" & clsCommon.GetMulcallString(arrMCCRights) & "))"
            txtMultBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("@BMC", qry, "MCC_Code", "MCC_NAME", txtMultBMC.arrValueMember, txtMultBMC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class