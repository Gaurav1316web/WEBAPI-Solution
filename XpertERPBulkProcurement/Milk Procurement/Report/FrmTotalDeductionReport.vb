Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient

Public Class FrmTotalDeductionReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim dt As DataTable

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
                sQuery = "  Select  VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME, " & DescName3 & "
                            from 
                             (Select *,0 as SweetQty,0 as SourQty,0 as CurdQty from (Select max(Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(Gender)Gender,"
                sQuery += "VSP_CODE,max(DCSCode)DCSCode,max(VSP_NAME)VSP_NAME,"
                sQuery += "sum(Milk_Qty)Milk_Qty,sum(Milk_Amount)Milk_Amount,sum(Head_Load_Amount)Head_Load_Amount,sum(Payable_Amount)Payable_Amount,sum(Deduction_Amount)Deduction_Amount,sum(Credit_Note_Amount)Credit_Note_Amount, " & DescName1 & " from(Select Registered_PDCS_CLUSTER,Gender,"
                sQuery += " VSP_CODE,DCSCode,VSP_NAME,"
                sQuery += "Milk_Qty,Milk_Amount,Head_Load_Amount,Payable_Amount,Deduction_Amount,Credit_Note_Amount, " & DescName2 & " from (Select max(yy.Registered_PDCS_CLUSTER)Registered_PDCS_CLUSTER,max(yy.Gender)Gender,"
                sQuery += "MAX(yy.VSP_CODE)VSP_CODE,max(yy.VLC_CODE_Uploader)DCSCode,max(yy.VSP_NAME)VSP_NAME,"
                sQuery += " SUM(yy.Milk_Qty)Milk_Qty,sum(yy.Milk_Amount)Milk_Amount,
                                   sum(yy.Head_Load_Amount)Head_Load_Amount,sum(yy.Payable_Amount)Payable_Amount,sum(yy.Deduction_Amount)Deduction_Amount,
                                   sum(yy.Credit_Note_Amount)Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount 
                                   from (   Select max(Registered_PDCS_CLUSTER) as Registered_PDCS_CLUSTER,max(Gender) as Gender "
                sQuery += " ,max(VLC_Code_VLC_Uploader) as VLC_CODE_Uploader,Vendor_CODE as VSP_CODE,max(Vendor_Name)VSP_NAME"
                sQuery += " ,Sum(Milk_Qty)as Milk_Qty,sum( Milk_Amount) as Milk_Amount,SUM(Head_Load_Amount) as Head_Load_Amount,SUM(Payable_Amount) as Payable_Amount ,
                                   SUM(Deduction_Amount) as Deduction_Amount,sUM(Credit_Note_Amount)as Credit_Note_Amount,DCS_Addition_Deduction,max(DCSDescription)DCSDescription,sum(Amount)Amount ,MAX(From_Date)From_Date 
                                   from  ( " & Qry1 & "  )xx where 2=2 and Doc_No IN (" & Document & ")"
                sQuery += " group by xx.Doc_No,"
                sQuery += " xx.Vendor_CODE,"
                sQuery += " xx.DCS_Addition_Deduction )YY group by "
                sQuery += " VSP_CODE,"
                sQuery += "DCS_Addition_Deduction)Tab1 
                        PIVOT(SUM(Amount) FOR DCS_Addition_Deduction IN (" & Description & ")) AS Tab2 )tmp group by "
                sQuery += " VSP_CODE "
                sQuery += ")YY "
                sQuery += " )Tab2 group by "
                sQuery += " VSP_CODE "
                sQuery += " order by cast(max(DCSCode)  as int) "

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
                    'EnableDisableControls(False)
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

    Private Sub EnableDisableControls(ByVal val As Boolean)
        txtDCS.Enabled = val
        txtMCC.Enabled = val
        TxtDeduction.Enabled = val
        RadGroupBox1.Enabled = val
    End Sub

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

    Private Sub txtDCS_Click(sender As Object, e As EventArgs) Handles txtDCS.Click
        Try
            Dim qry As String = " select  TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code] ,TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name],TSPL_VLC_MASTER_HEAD.VSP_Code,isnull(TSPL_VENDOR_MASTER.Zone_Code,'') AS Zone
		                          from TSPL_VLC_MASTER_HEAD 
		                          left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code"

            txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "VSP_Code", "DCS Name", txtDCS.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Private Sub TxtDeduction_Click(sender As Object, e As EventArgs) Handles TxtDeduction.Click
        Try
            Dim qry As String = " Select Code,Description from TSPL_DEDUCTION_MASTER "

            TxtDeduction.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "Code", "Description", TxtDeduction.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmTotalDeductionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
    End Sub
End Class