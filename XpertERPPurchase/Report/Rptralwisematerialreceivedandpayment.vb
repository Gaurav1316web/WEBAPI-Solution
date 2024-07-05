Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class Rptralwisematerialreceivedandpayment
    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Code", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        'qry += " where 2=2 and Seg_No = '7' AND GIT='N'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub TxtMultiSelectFinder1__My_Click_1(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder1._My_Click
        Dim qry As String = " select DocumentCode as Ral from tspl_tender_header  "
        TxtMultiSelectFinder1.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Ral", "Ral", TxtMultiSelectFinder1.arrValueMember, TxtMultiSelectFinder1.arrDispalyMember)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtLocation.arrValueMember = Nothing
        txtVendor.arrValueMember = Nothing
        TxtMultiSelectFinder1.arrValueMember = Nothing
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember) + " "))
            End If


            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))



            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember) + " "))
            End If

            If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(TxtMultiSelectFinder1.arrDispalyMember) + " "))
            End If

            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor Payment Details Report", gv1, arrHeader, "Vendor Payment Details Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    'Private Sub RadPageView1_SelectedPageChanged(sender As Object, e As EventArgs) Handles RadPageView1.SelectedPageChanged
    '    SetUserMgmtNew()
    '    txtToDate.Value = clsCommon.GETSERVERDATE()
    '    txtFromDate.Value = clsCommon.GETSERVERDATE()
    'End Sub

    Private Sub rptralwisematerialreceivedandpayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetUserMgmtNew()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1

            Dim strqry As String = "With CTETemp as ( 
                   select  zzz.RAL_NO,zzz.[Name Of Supplier],zzz.Supplied_Material,zzz.Weighment_Code,zzz.Weighment_Date,zzz.Net_Weight,zzz.Bill_No,zzz.Bill_date,zzz.Amount,zzz.Due_Date,zzz.Release_date,CASE WHEN zzz.[Delay day] > 0 THEN CONVERT(varchar, zzz.[Delay day]) ELSE 'No Delay' END AS [Delay day] from (" & Environment.NewLine & "
                   select  TSPL_PI_HEAD.Ref_No AS RAL_NO,TSPL_VENDOR_MASTER.Vendor_Name as [Name Of Supplier],TSPL_PI_DETAIL.Item_Desc as Supplied_Material,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight,TSPL_GRN_HEAD.[Invoice/Challan_No] as Bill_No,TSPL_GRN_HEAD.Invoice_Date as Bill_date,TSPL_PI_DETAIL.Taxable_Amount as Amount,TSPL_VENDOR_INVOICE_HEAD.Due_Date as Due_date,TSPL_PAYMENT_HEADER.Payment_Date as Release_date," & Environment.NewLine & "
                   CASE WHEN isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' THEN  CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_No ,'')<>'' THEN  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Due_Date,103 ),TSPL_PAYMENT_HEADER.Payment_Date) ELSE  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Due_Date,103 ),convert(date,GETDATE(),103)) END   ELSE NULL END AS [Delay day] " & Environment.NewLine & "
                   from TSPL_VENDOR_INVOICE_HEAD" & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No " & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No " & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_GRN_HEAD ON TSPL_GRN_HEAD.GRN_No=TSPL_PI_DETAIL.GRN_ID " & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_HEAD ON TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No " & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_DETAIL ON TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code " & Environment.NewLine & "
                   left outer join TSPL_PAYMENT_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_PAYMENT_DETAIL .Document_No  " & Environment.NewLine & "
                   left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No  " & Environment.NewLine & "
                   left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_VENDOR_INVOICE_HEAD.Terms_Code " & Environment.NewLine & "
                   left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD .Vendor_Code " & Environment.NewLine & "
                   where  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " & Environment.NewLine & "
                   and isnull(TSPL_PAYMENT_HEADER .IsChkReverse ,'')='N' and TSPL_PAYMENT_HEADER.Posted =1 "

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strqry += " and TSPL_VENDOR_MASTER.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strqry += " and TSPL_PI_DETAIL.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
                strqry += " and TSPL_GRN_HEAD.Ref_No in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder1.arrValueMember) + ")"
            End If

            strqry += " )zzz ) " & Environment.NewLine & "
                   Select ROW_NUMBER() Over (Order By (Select 1)) As SNo,CTETemp.* " & Environment.NewLine & "
                   from CTETemp " & Environment.NewLine & "
                   where 1=1  " & Environment.NewLine & "
                   group by 
                   CTETemp.RAL_NO,CTETemp.[Name Of Supplier],CTETemp.Supplied_Material,CTETemp.Weighment_Code,CTETemp.Weighment_Date,CTETemp.Net_Weight,CTETemp.Bill_No,CTETemp.Bill_date,CTETemp.Amount,CTETemp.Due_Date,CTETemp.Release_date,CTETemp.[Delay day]  "

            'Dim strqry As String = "With CTETemp as ( " & Environment.NewLine &
            '" select  zzz.RAL_NO,zzz.[Name Of Supplier],zzz.Supplied_Material,zzz.Weighment_Code,zzz.Weighment_Date,zzz.Net_Weight,zzz.Bill_No,zzz.Bill_date,zzz.Amount,zzz.Due_Date,zzz.Release_date,zzz.[Delay day] as [Delay day] from (" & Environment.NewLine &
            '" select  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code ,TSPL_VENDOR_MASTER.GSTFinalNo as [Vendor Gst No],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name] ," & Environment.NewLine &
            '" case when isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' then  TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date else null end as [Vendor Invoice date],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [Vendor Invoice No],TSPL_VENDOR_INVOICE_HEAD.Document_Total as [Invoice value],TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount as [Taxable value],TSPL_VENDOR_MASTER.State as [State]  ,TSPL_VENDOR_INVOICE_HEAD.TAX1_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX2_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX3_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX4_Rate as [GST Rate],CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_VENDOR_INVOICE_HEAD.Total_Tax  as [GST Amount],TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount AS TDS,TSPL_VENDOR_INVOICE_HEAD.Document_Total -TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount AS [Net payable] ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP Document No] ,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as [AP Document date],TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  as [PI Document No], TSPL_VENDOR_INVOICE_HEAD.Terms_Code AS [Payment Terms] ,isnull(TSPL_TERMS_MASTER.No_Days,0) AS [Credit Period]" & Environment.NewLine &
            '" ,TSPL_PAYMENT_HEADER.Payment_No ,TSPL_PAYMENT_HEADER.Payment_Date ,TSPL_PAYMENT_DETAIL .Applied_Amount ,TSPL_VENDOR_INVOICE_HEAD.Loc_Code  ," & Environment.NewLine &
            '" CASE WHEN isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' THEN  CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_No ,'')<>'' THEN  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103 ),TSPL_PAYMENT_HEADER.Payment_Date) ELSE  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103 ),GETDATE()) END   ELSE NULL END AS [Days over due]" & Environment.NewLine &
            '" from TSPL_VENDOR_INVOICE_HEAD" & Environment.NewLine &
            '" left outer join TSPL_PAYMENT_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_PAYMENT_DETAIL .Document_No " & Environment.NewLine &
            '" left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No " & Environment.NewLine &
            '" left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_VENDOR_INVOICE_HEAD.Terms_Code" & Environment.NewLine &
            '" left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD .Vendor_Code" & Environment.NewLine &
            '" where  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " & Environment.NewLine &
            '" and isnull(TSPL_PAYMENT_HEADER .IsChkReverse ,'')='N' and TSPL_PAYMENT_HEADER.Posted =1 " & Environment.NewLine &
            '" )zzz )" & Environment.NewLine &
            '" Select CTETemp.*,max(isnull(T1.[Net payable] ,0))-sum( isnull(T1.[Payment amount]  ,0)) as  [Balance amount ] " & Environment.NewLine &
            '" from CTETemp " & Environment.NewLine &
            '" left outer join  CTETemp T1 on t1.[AP Document No] =CTETemp.[AP Document No] AND T1.RowNo<=CTETemp.RowNo " & Environment.NewLine &
            '" where 1=1 " & Environment.NewLine



            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    strqry += " and CTETemp.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            'End If
            'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            '    strqry += " and CTETemp.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
            'End If

            'strqry += "  group by CTETemp.RAL_NO,CTETemp.[Name Of Supplier],CTETemp.Supplied_Material,CTETemp.Weighment_Code,CTETemp.Weighment_Date,CTETemp.Net_Weight,CTETemp.Bill_No,CTETemp.Bill_date,CTETemp.Amount 
            '             order by CTETemp.[AP Document No],CTETemp.[AP Document date]"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                'gv1.Columns("RowNo").IsVisible = False
                'gv1.Columns("Vendor_Code").IsVisible = False

                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub



    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim qry As String = " "

        Try
            qry = " With CTETemp as ( 
                   select  zzz.Location_Desc,zzz.Add1,zzz.Add2,zzz.Add3,zzz.RAL_NO,zzz.[Name Of Supplier],zzz.Supplied_Material,zzz.Weighment_Code,zzz.Weighment_Date,zzz.Net_Weight,zzz.Bill_No,zzz.Bill_date,zzz.Amount,zzz.Due_Date,zzz.Release_date,CASE WHEN zzz.[Delay day] > 0 THEN CONVERT(varchar, zzz.[Delay day]) ELSE 'No Delay' END AS [Delay day] from (" & Environment.NewLine & "
                   select  TSPL_LOCATION_MASTER.Location_Desc as Location_Desc,TSPL_LOCATION_MASTER.Add1 as Add1,TSPL_LOCATION_MASTER.Add2 as Add2,TSPL_LOCATION_MASTER.Add3 as Add3,TSPL_PI_HEAD.Ref_No AS RAL_NO,TSPL_VENDOR_MASTER.Vendor_Name as [Name Of Supplier],TSPL_PI_DETAIL.Item_Desc as Supplied_Material,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight,TSPL_GRN_HEAD.[Invoice/Challan_No] as Bill_No,TSPL_GRN_HEAD.Invoice_Date as Bill_date,TSPL_PI_DETAIL.Taxable_Amount as Amount,TSPL_VENDOR_INVOICE_HEAD.Due_Date as Due_date,TSPL_PAYMENT_HEADER.Payment_Date as Release_date," & Environment.NewLine & "
                   CASE WHEN isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' THEN  CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_No ,'')<>'' THEN  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Due_Date,103 ),TSPL_PAYMENT_HEADER.Payment_Date) ELSE  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Due_Date,103 ),convert(date,GETDATE(),103)) END   ELSE NULL END AS [Delay day] " & Environment.NewLine & "
                   from TSPL_VENDOR_INVOICE_HEAD" & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No " & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No " & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_GRN_HEAD ON TSPL_GRN_HEAD.GRN_No=TSPL_PI_DETAIL.GRN_ID " & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_HEAD ON TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No " & Environment.NewLine & "
                   LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_DETAIL ON TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code " & Environment.NewLine & "
                   left outer join TSPL_PAYMENT_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_PAYMENT_DETAIL .Document_No  " & Environment.NewLine & "
                   left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No  " & Environment.NewLine & "
                   left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_VENDOR_INVOICE_HEAD.Terms_Code " & Environment.NewLine & "
                   left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD .Vendor_Code " & Environment.NewLine & "
                   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code " & Environment.NewLine & "
                   where  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " & Environment.NewLine & "
                   and isnull(TSPL_PAYMENT_HEADER .IsChkReverse ,'')='N' and TSPL_PAYMENT_HEADER.Posted =1 "

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += " and TSPL_VENDOR_MASTER.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_PI_DETAIL.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
                qry += " and TSPL_GRN_HEAD.Ref_No in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder1.arrValueMember) + ")"
            End If

            qry += " )zzz ) " & Environment.NewLine & "
                   Select ROW_NUMBER() Over (Order By (Select 1)) As SNo,CTETemp.* " & Environment.NewLine & "
                   from CTETemp " & Environment.NewLine & "
                   where 1=1  " & Environment.NewLine & "
                   group by 
                   CTETemp.Location_Desc,CTETemp.Add1,CTETemp.Add2,CTETemp.Add3,CTETemp.RAL_NO,CTETemp.[Name Of Supplier],CTETemp.Supplied_Material,CTETemp.Weighment_Code,CTETemp.Weighment_Date,CTETemp.Net_Weight,CTETemp.Bill_No,CTETemp.Bill_date,CTETemp.Amount,CTETemp.Due_Date,CTETemp.Release_date,CTETemp.[Delay day]"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.Purchase, dt, "Rptralwisematerialreceivedandpayment", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
