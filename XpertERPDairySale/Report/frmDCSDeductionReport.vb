Imports common
Public Class frmDCSDeductionReport
    Dim AreaWiseBilling As Boolean = False
    Private Sub frmDCSDeductionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtFromDate.Value.AddMonths(1)
            AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
            lblArea.Enabled = AreaWiseBilling
            txtMultArea.Enabled = AreaWiseBilling
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Try
            EnableDisableFields(True)
            Reset()
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        BlankGrid()
    End Sub

    Sub EnableDisableFields(ByVal isBool As Boolean)
        RadGroupBox1.Enabled = isBool
        RadGroupBox2.Enabled = isBool
        btngo.Enabled = isBool
    End Sub

    Sub BlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
    End Sub

    Private Sub txtMultMCC__My_Click(sender As Object, e As EventArgs) Handles txtMultMCC._My_Click
        Try
            Dim strQry As String = "select MCC_Code As Code,MCC_NAME As Name,Area_Location_Code As [Area Location Code] From tspl_mcc_master where 1=1 "
            If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
                strQry &= " And Area_Location_Code In (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ")"
            End If
            txtMultMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("@MCC", strQry, "Code", "Name", txtMultMCC.arrValueMember, txtMultMCC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Try
            Dim strQry As String = "Select TSPL_VLC_MASTER_HEAD.VSP_Code As Code,TSPL_VLC_MASTER_HEAD.VLC_Name As Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [DCS Uploader Code] from TSPL_VLC_MASTER_HEAD Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code where TSPL_VENDOR_MASTER.Vendor_Group_Code='DCS'"
            txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("@DCS", strQry, "Code", "Name", txtMultDCS.arrValueMember, txtMultDCS.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultDed__My_Click(sender As Object, e As EventArgs) Handles txtMultDed._My_Click
        Try
            Dim strQry As String = "select Code,Description from TSPL_DEDUCTION_MASTER Where Ded_Grp_Code='Deduction' Order By Code"
            txtMultDed.arrValueMember = clsCommon.ShowMultipleSelectForm("@Deduction", strQry, "Code", "Description", txtMultDed.arrValueMember, txtMultDed.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultArea__My_Click(sender As Object, e As EventArgs) Handles txtMultArea._My_Click
        Try
            Dim strQry As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc As Name from TSPL_LOCATION_MASTER where 1=1 "
            strQry &= " And TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'"
            txtMultArea.arrValueMember = clsCommon.ShowMultipleSelectForm("@Area", strQry, "Code", "Name", txtMultArea.arrValueMember, txtMultArea.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        ReportQry(False)
    End Sub

    Sub ReportQry(ByVal isPrint As Boolean)
        Try
            Dim Qry As String = Nothing
            Qry = "Select ROW_NUMBER() Over (Order By (Select 1)) As SNo,Bill_No As [Bill No],Document_Date As [Date],VLC_Name As [DCS Name],VLC_Code_VLC_Uploader As [Code],"
            If chkGSTReport.Checked Then
                Qry &= " GSTFinalNo As [GST No],Deduction As [Deduction Code],Description As [Deduction Name],"
            Else
                If isPrint Then
                    Qry &= " Deduction As [Deduction Code],Description As [Deduction Name],"
                End If
                Qry &= " 0 As [Ded Balance],"
            End If
            Qry &= " Amount_Less_Discount As Amount,"
            If chkGSTReport.Checked OrElse isPrint Then
                Qry &= " Convert(decimal(18,2),Case When TAX1 ='MNDTAX' Then TAX1_Amt
When TAX2 ='MNDTAX' Then TAX2_Amt
When TAX3 ='MNDTAX' Then TAX3_Amt
When TAX4 ='MNDTAX' Then TAX4_Amt
When TAX5 ='MNDTAX' Then TAX5_Amt
When TAX6 ='MNDTAX' Then TAX6_Amt
When TAX7 ='MNDTAX' Then TAX7_Amt
When TAX8 ='MNDTAX' Then TAX8_Amt
When TAX9 ='MNDTAX' Then TAX9_Amt
When TAX10 ='MNDTAX' Then TAX10_Amt Else 0 End) As [MANDI TAX],
Convert(decimal(18,2),Case When TAX1 ='KKF' Then TAX1_Amt
When TAX2 ='KKF' Then TAX2_Amt
When TAX3 ='KKF' Then TAX3_Amt
When TAX4 ='KKF' Then TAX4_Amt
When TAX5 ='KKF' Then TAX5_Amt
When TAX6 ='KKF' Then TAX6_Amt
When TAX7 ='KKF' Then TAX7_Amt
When TAX8 ='KKF' Then TAX8_Amt
When TAX9 ='KKF' Then TAX9_Amt
When TAX10 ='KKF' Then TAX10_Amt Else 0 End) As [KKF],
Convert(decimal(18,2),Case When TAX1 ='CGST' Then TAX1_Amt
When TAX2 ='CGST' Then TAX2_Amt
When TAX3 ='CGST' Then TAX3_Amt
When TAX4 ='CGST' Then TAX4_Amt
When TAX5 ='CGST' Then TAX5_Amt
When TAX6 ='CGST' Then TAX6_Amt
When TAX7 ='CGST' Then TAX7_Amt
When TAX8 ='CGST' Then TAX8_Amt
When TAX9 ='CGST' Then TAX9_Amt
When TAX10 ='CGST' Then TAX10_Amt Else 0 End) As [CGST],
Convert(decimal(18,2),Case When TAX1 ='SGST' Then TAX1_Amt
When TAX2 ='SGST' Then TAX2_Amt
When TAX3 ='SGST' Then TAX3_Amt
When TAX4 ='SGST' Then TAX4_Amt
When TAX5 ='SGST' Then TAX5_Amt
When TAX6 ='SGST' Then TAX6_Amt
When TAX7 ='SGST' Then TAX7_Amt
When TAX8 ='SGST' Then TAX8_Amt
When TAX9 ='SGST' Then TAX9_Amt
When TAX10 ='SGST' Then TAX10_Amt Else 0 End) As [SGST],
Convert(decimal(18,2),Case When TAX1 ='IGST' Then TAX1_Amt
When TAX2 ='IGST' Then TAX2_Amt
When TAX3 ='IGST' Then TAX3_Amt
When TAX4 ='IGST' Then TAX4_Amt
When TAX5 ='IGST' Then TAX5_Amt
When TAX6 ='IGST' Then TAX6_Amt
When TAX7 ='IGST' Then TAX7_Amt
When TAX8 ='IGST' Then TAX8_Amt
When TAX9 ='IGST' Then TAX9_Amt
When TAX10 ='IGST' Then TAX10_Amt Else 0 End) As [IGST],
IsNull(Transporter_Commission_TotalAmt,0) As TPT,"
            End If
            Qry &= " Total_Amt As [Total Amount]"
            If Not chkGSTReport.Checked OrElse isPrint Then
                Qry &= " ,Recommended_By As [Recommendance]"
            End If
            If isPrint Then
                Qry &= " ,Logo_Img,Logo_Img2,Comp_Name,Add1,Add2,Add3,STATE_NAME,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & "' As fromDate, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") & "' As todate,'" & objCommonVar.CurrentUser & "' As PrintBy "
            End If
            Qry &= " from(" & ReturnBaseQry() & ")finalQry"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If isPrint Then
                    Dim frm As New frmCrystalReportViewer()
                    If chkGSTReport.Checked Then
                        frm.funreport(Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "crptDCSDeductionGSTReport", "DCS GST Report")
                    Else
                        frm.funreport(Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "crptDCSDeductionReport", "DCS Deduction Report")
                    End If
                    frm = Nothing
                Else
                    BlankGrid()
                    gv1.DataSource = dt
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    SetGridFormat()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    EnableDisableFields(False)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        Try
            gv1.AutoExpandGroups = False
            gv1.ShowGroupPanel = False
            gv1.ShowRowHeaderColumn = False
            gv1.AllowAddNewRow = False
            gv1.AllowDeleteRow = False
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = True
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).BestFit()
            Next
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = False
            'Dim summaryRowItem As New GridViewSummaryRowItem()
            'summaryRowItem.Add(New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum))
            'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function ReturnBaseQry() As String
        Dim BaseQry As String = "Select TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,
TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_MASTER.GSTFinalNo,TSPL_SD_SHIPMENT_HEAD.Recommended_By,
TSPL_SD_SHIPMENT_HEAD.Deduction,TSPL_DEDUCTION_MASTER.Description,
TSPL_SD_SHIPMENT_HEAD.Deduction +'-'+RIGHT(TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No, 4) As Bill_No,
TSPL_SD_SHIPMENT_DETAIL.Item_Code,
TSPL_SD_SHIPMENT_DETAIL.TAX1,TSPL_SD_SHIPMENT_DETAIL.TAX1_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt,
TSPL_SD_SHIPMENT_DETAIL.TAX2,TSPL_SD_SHIPMENT_DETAIL.TAX2_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt,
TSPL_SD_SHIPMENT_DETAIL.TAX3,TSPL_SD_SHIPMENT_DETAIL.TAX3_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt,
TSPL_SD_SHIPMENT_DETAIL.TAX4,TSPL_SD_SHIPMENT_DETAIL.TAX4_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt,
TSPL_SD_SHIPMENT_DETAIL.TAX5,TSPL_SD_SHIPMENT_DETAIL.TAX5_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt,
TSPL_SD_SHIPMENT_DETAIL.TAX6,TSPL_SD_SHIPMENT_DETAIL.TAX6_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt,
TSPL_SD_SHIPMENT_DETAIL.TAX7,TSPL_SD_SHIPMENT_DETAIL.TAX7_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt,
TSPL_SD_SHIPMENT_DETAIL.TAX8,TSPL_SD_SHIPMENT_DETAIL.TAX8_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt,
TSPL_SD_SHIPMENT_DETAIL.TAX9,TSPL_SD_SHIPMENT_DETAIL.TAX9_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt,
TSPL_SD_SHIPMENT_DETAIL.TAX10,TSPL_SD_SHIPMENT_DETAIL.TAX10_Rate,TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt1,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt2,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt3,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt4,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt5,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt6,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt7,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt8,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt9,
TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt10,
TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount,TSPL_SD_SHIPMENT_HEAD.Total_Tax_Amt,
TSPL_SD_SHIPMENT_HEAD.Transporter_Commission_TotalAmt,
TSPL_SD_SHIPMENT_HEAD.Total_Amt,
TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_STATE_MASTER.STATE_NAME
from TSPL_SD_SHIPMENT_DETAIL
Left Outer Join TSPL_SD_SHIPMENT_HEAD On TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
LEFT Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
LEFT Outer Join TSPL_CUSTOMER_VENDOR_MAPPING On TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
LEFT Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
Left Outer Join TSPL_DEDUCTION_MASTER On TSPL_DEDUCTION_MASTER.Code=TSPL_SD_SHIPMENT_HEAD.Deduction And TSPL_DEDUCTION_MASTER.Ded_Grp_Code='Deduction'
Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'
Left Outer Join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State
Where 1=1
And CONVERT(DATE,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' And CONVERT(DATE,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "
        If txtMultDed.arrValueMember IsNot Nothing AndAlso txtMultDed.arrValueMember.Count > 0 Then
            BaseQry &= " And TSPL_DEDUCTION_MASTER.Code In (" & clsCommon.GetMulcallString(txtMultDed.arrValueMember) & ") "
        End If
        BaseQry &= " And TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader In ( select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader
 from TSPL_VLC_MASTER_HEAD 
 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code
 left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc where 1=1 "
        If txtMultArea.arrValueMember IsNot Nothing AndAlso txtMultArea.arrValueMember.Count > 0 Then
            BaseQry &= " And TSPL_MCC_MASTER.Area_Location_Code In (" & clsCommon.GetMulcallString(txtMultArea.arrValueMember) & ") "
        End If
        If txtMultMCC.arrValueMember IsNot Nothing AndAlso txtMultMCC.arrValueMember.Count > 0 Then
            BaseQry &= " And TSPL_MCC_MASTER.MCC_Code In (" & clsCommon.GetMulcallString(txtMultMCC.arrValueMember) & ") "
        End If
        If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
            BaseQry &= " And TSPL_VLC_MASTER_HEAD.VSP_Code In (" & clsCommon.GetMulcallString(txtMultDCS.arrValueMember) & ") "
        End If
        BaseQry &= " )"
        Return BaseQry
    End Function

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ExportExcelPDF(False)
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        ExportExcelPDF(True)
    End Sub

    Sub ExportExcelPDF(ByVal isPDF As Boolean)
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmDCSDeductionReport & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()
            If Not isPDF Then
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Report Name : " & strHeading)
            End If
            arrHeader.Add("Date Range from : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & " To " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            If isPDF Then
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToExcel(Nothing, gv1, arrHeader, strHeading)
                'transportSql.exportdata(gv1, "", Me.Text, False, arrHeader, False, False, True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        ReportQry(True)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class