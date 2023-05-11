Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'' RICHA AGARWAL UDL/18/07/19-000306 created on 19 July,2019 
Public Class RptVendorPaymentDetails
    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim variable1 As String = Nothing
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click

        Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
        qry += " where 2=2 and Seg_No = '7' AND GIT='N'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Code", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub rptAPReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Reset()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtLocation.arrValueMember = Nothing
        txtVendor.arrValueMember = Nothing
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1

            '            Dim strqry As String = "with CTETemp as (" & Environment.NewLine & _
            '            " select " & Environment.NewLine & _
            '            " TSPL_VENDOR_INVOICE_HEAD.Vendor_Code ,TSPL_VENDOR_MASTER.GSTFinalNo as [Vendor Gst No],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name] ," & Environment.NewLine & _
            '            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' then  TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date else null end as [Vendor Invoice date],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [Vendor Invoice No],TSPL_VENDOR_INVOICE_HEAD.Document_Total as [Invoice value],TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount as [Taxable value],TSPL_VENDOR_MASTER.State as [State]  ,TSPL_VENDOR_INVOICE_HEAD.TAX1_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX2_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX3_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX4_Rate as [GST Rate],CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_VENDOR_INVOICE_HEAD.Total_Tax  as [GST Amount],TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount AS TDS,TSPL_VENDOR_INVOICE_HEAD.Document_Total -TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount AS [Net payable] ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP Document No] ,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as [AP Document date],TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  as [PI Document No], TSPL_VENDOR_INVOICE_HEAD.Terms_Code AS [Payment Terms] ,isnull(TSPL_TERMS_MASTER.No_Days,0) AS [Credit Period]" & Environment.NewLine & _
            '            " ,TSPL_PAYMENT_HEADER.Payment_No ,TSPL_PAYMENT_HEADER.Payment_Date ,TSPL_PAYMENT_DETAIL .Applied_Amount ,TSPL_VENDOR_INVOICE_HEAD.Loc_Code  ," & Environment.NewLine & _
            '            " CASE WHEN isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' THEN  CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_No ,'')<>'' THEN  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103 ),TSPL_PAYMENT_HEADER.Payment_Date) ELSE  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103 ),GETDATE()) END   ELSE NULL END AS [Days over due]," & Environment.NewLine & _
            '            " ROW_NUMBER() OVER (PARTITION BY TSPL_VENDOR_INVOICE_HEAD.Document_No ORDER BY TSPL_VENDOR_INVOICE_HEAD.Document_No, convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103))  as RowNo,(TSPL_VENDOR_INVOICE_HEAD.Document_Total -TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount-TSPL_PAYMENT_DETAIL .Applied_Amount) as bal" & Environment.NewLine & _
            '            " from TSPL_VENDOR_INVOICE_HEAD" & Environment.NewLine & _
            '            " left outer join TSPL_PAYMENT_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_PAYMENT_DETAIL .Document_No " & Environment.NewLine & _
            '            " left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No " & Environment.NewLine & _
            '            " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_VENDOR_INVOICE_HEAD.Terms_Code" & Environment.NewLine & _
            '            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD .Vendor_Code" & Environment.NewLine & _
            '            " where " & Environment.NewLine & _
            '            " ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " & Environment.NewLine & _
            '" and isnull(TSPL_PAYMENT_HEADER .IsChkReverse ,'')='N' and TSPL_PAYMENT_HEADER.Posted =1 " & Environment.NewLine & _
            ' " )" & Environment.NewLine & _
            '            " select CTETemp.[Vendor Gst No] ,CTETemp.[Vendor Name] ,CTETemp.[Vendor Invoice date] ,CTETemp.[Vendor Invoice No] ,CTETemp.[Invoice value] ,CTETemp.[Taxable value] ,CTETemp.State,CTETemp.[GST Rate],CTETemp.[GST Amount],CTETemp.TDS,CTETemp.[Net payable] ,CTETemp.[AP Document No],CTETemp.[AP Document date],CTETemp.[PI Document No],CTETemp.[Payment Terms] ,CTETemp.[Credit Period],CTETemp.Payment_No as [Payment Document No],CTETemp.Payment_Date as [Payment document date],CTETemp.Applied_Amount as [Payment amount], " & Environment.NewLine & _
            '            " case when RowNo >1 then SUM(convert(decimal(18,2),Applied_Amount )-convert(decimal(18,2),bal )) Over (Partition by [AP Document No] ORDER BY RowNo) else   SUM(convert(decimal(18,2),[Net payable])-convert(decimal(18,2),Applied_Amount )) Over (Partition by [AP Document No] ORDER BY RowNo) end as [Balance amount ]," & Environment.NewLine & _
            '            " CTETemp.Loc_Code as [Location] ,CTETemp.[Days over due] " & Environment.NewLine & _
            '            " from CTETemp where 1=1 " & Environment.NewLine

            ''richa UDL/05/09/19-000316
            ''Dim strqry As String = "With CTETemp as ( " & Environment.NewLine & _
            '" select  zzz.Vendor_Code,zzz.[Vendor Gst No] ,zzz.[Vendor Name] ,zzz.[Vendor Invoice date] ,zzz.[Vendor Invoice No] ,zzz.[Invoice value] ,zzz.[Taxable value] ,zzz.State,zzz.[GST Rate],zzz.[GST Amount],zzz.TDS,zzz.[Net payable] ,zzz.[AP Document No],zzz.[AP Document date],zzz.[PI Document No],zzz.[Payment Terms] ,zzz.[Credit Period],zzz.Payment_No as [Payment Document No],zzz.Payment_Date as [Payment document date],zzz.Applied_Amount as [Payment amount] , ROW_NUMBER() Over (Partition By zzz.[AP Document No] ORDER BY zzz.[AP Document No],convert(date,zzz.[AP Document date],103),zzz.Payment_No) RowNo,zzz.Loc_Code as [Location],zzz.[Days over due] as [Days over due] from (" & Environment.NewLine & _
            '" select  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code ,TSPL_VENDOR_MASTER.GSTFinalNo as [Vendor Gst No],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name] ," & Environment.NewLine & _
            '" case when isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' then  TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date else null end as [Vendor Invoice date],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [Vendor Invoice No],TSPL_VENDOR_INVOICE_HEAD.Document_Total as [Invoice value],TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount as [Taxable value],TSPL_VENDOR_MASTER.State as [State]  ,TSPL_VENDOR_INVOICE_HEAD.TAX1_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX2_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX3_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX4_Rate as [GST Rate],CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_VENDOR_INVOICE_HEAD.Total_Tax  as [GST Amount],TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount AS TDS,TSPL_VENDOR_INVOICE_HEAD.Document_Total -TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount AS [Net payable] ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP Document No] ,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as [AP Document date],TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  as [PI Document No], TSPL_VENDOR_INVOICE_HEAD.Terms_Code AS [Payment Terms] ,isnull(TSPL_TERMS_MASTER.No_Days,0) AS [Credit Period]" & Environment.NewLine & _
            '" ,TSPL_PAYMENT_HEADER.Payment_No ,TSPL_PAYMENT_HEADER.Payment_Date ,TSPL_PAYMENT_DETAIL .Applied_Amount ,TSPL_VENDOR_INVOICE_HEAD.Loc_Code  ," & Environment.NewLine & _
            '" CASE WHEN isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' THEN  CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_No ,'')<>'' THEN  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103 ),TSPL_PAYMENT_HEADER.Payment_Date) ELSE  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103 ),GETDATE()) END   ELSE NULL END AS [Days over due]" & Environment.NewLine & _
            '" from TSPL_VENDOR_INVOICE_HEAD" & Environment.NewLine & _
            '" left outer join TSPL_PAYMENT_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_PAYMENT_DETAIL .Document_No " & Environment.NewLine & _
            '" left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No " & Environment.NewLine & _
            '" left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_VENDOR_INVOICE_HEAD.Terms_Code" & Environment.NewLine & _
            '" left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD .Vendor_Code" & Environment.NewLine & _
            '" where  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " & Environment.NewLine & _
            '" and isnull(TSPL_PAYMENT_HEADER .IsChkReverse ,'')='N' and TSPL_PAYMENT_HEADER.Posted =1 " & Environment.NewLine & _
            '" )zzz )" & Environment.NewLine & _
            '" select *,(Select max(isnull([Net payable] ,0))-sum( isnull([Payment amount]  ,0)) from CTETemp T1 WHERE  T1.[AP Document No]=CTETemp.[AP Document No]" & Environment.NewLine & _
            '" AND T1.RowNo<=CTETemp.RowNo) as [Balance amount ] " & Environment.NewLine & _
            '" from CTETemp " & Environment.NewLine & _
            '" where 1=1 " & Environment.NewLine

            Dim strqry As String = "With CTETemp as ( " & Environment.NewLine & _
            " select  zzz.Vendor_Code,zzz.[Vendor Gst No] ,zzz.[Vendor Name] ,zzz.[Vendor Invoice date] ,zzz.[Vendor Invoice No] ,zzz.[Invoice value] ,zzz.[Taxable value] ,zzz.State,zzz.[GST Rate],zzz.[GST Amount],zzz.TDS,zzz.[Net payable] ,zzz.[AP Document No],zzz.[AP Document date],zzz.[PI Document No],zzz.[Payment Terms] ,zzz.[Credit Period],zzz.Payment_No as [Payment Document No],zzz.Payment_Date as [Payment document date],zzz.Applied_Amount as [Payment amount] , ROW_NUMBER() Over (Partition By zzz.[AP Document No] ORDER BY zzz.[AP Document No],convert(date,zzz.[AP Document date],103),zzz.Payment_No) RowNo,zzz.Loc_Code as [Location],zzz.[Days over due] as [Days over due] from (" & Environment.NewLine & _
            " select  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code ,TSPL_VENDOR_MASTER.GSTFinalNo as [Vendor Gst No],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name] ," & Environment.NewLine & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' then  TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date else null end as [Vendor Invoice date],TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [Vendor Invoice No],TSPL_VENDOR_INVOICE_HEAD.Document_Total as [Invoice value],TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount as [Taxable value],TSPL_VENDOR_MASTER.State as [State]  ,TSPL_VENDOR_INVOICE_HEAD.TAX1_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX2_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX3_Rate +TSPL_VENDOR_INVOICE_HEAD.TAX4_Rate as [GST Rate],CASE WHEN TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' THEN -1 ELSE 1 END * TSPL_VENDOR_INVOICE_HEAD.Total_Tax  as [GST Amount],TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount AS TDS,TSPL_VENDOR_INVOICE_HEAD.Document_Total -TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount AS [Net payable] ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP Document No] ,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as [AP Document date],TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  as [PI Document No], TSPL_VENDOR_INVOICE_HEAD.Terms_Code AS [Payment Terms] ,isnull(TSPL_TERMS_MASTER.No_Days,0) AS [Credit Period]" & Environment.NewLine & _
            " ,TSPL_PAYMENT_HEADER.Payment_No ,TSPL_PAYMENT_HEADER.Payment_Date ,TSPL_PAYMENT_DETAIL .Applied_Amount ,TSPL_VENDOR_INVOICE_HEAD.Loc_Code  ," & Environment.NewLine & _
            " CASE WHEN isnull(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')<>'' THEN  CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_No ,'')<>'' THEN  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103 ),TSPL_PAYMENT_HEADER.Payment_Date) ELSE  DATEDIFF(DAY,CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103 ),GETDATE()) END   ELSE NULL END AS [Days over due]" & Environment.NewLine & _
            " from TSPL_VENDOR_INVOICE_HEAD" & Environment.NewLine & _
            " left outer join TSPL_PAYMENT_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_PAYMENT_DETAIL .Document_No " & Environment.NewLine & _
            " left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No " & Environment.NewLine & _
            " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_VENDOR_INVOICE_HEAD.Terms_Code" & Environment.NewLine & _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD .Vendor_Code" & Environment.NewLine & _
            " where  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " & Environment.NewLine & _
            " and isnull(TSPL_PAYMENT_HEADER .IsChkReverse ,'')='N' and TSPL_PAYMENT_HEADER.Posted =1 " & Environment.NewLine & _
            " )zzz )" & Environment.NewLine & _
            " Select CTETemp.*,max(isnull(T1.[Net payable] ,0))-sum( isnull(T1.[Payment amount]  ,0)) as  [Balance amount ] " & Environment.NewLine & _
            " from CTETemp " & Environment.NewLine & _
            " left outer join  CTETemp T1 on t1.[AP Document No] =CTETemp.[AP Document No] AND T1.RowNo<=CTETemp.RowNo " & Environment.NewLine & _
            " where 1=1 " & Environment.NewLine



            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strqry += " and CTETemp.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strqry += " and CTETemp.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
                            End If

            strqry += "  group by CTETemp.Vendor_Code,CTETemp.[Vendor Gst No] ,CTETemp.[Vendor Name] ,CTETemp.[Vendor Invoice date] ,CTETemp.[Vendor Invoice No] ,CTETemp.[Invoice value] ,CTETemp.[Taxable value] ,CTETemp.State,CTETemp.[GST Rate],CTETemp.[GST Amount],CTETemp.TDS,CTETemp.[Net payable] ,CTETemp.[AP Document No],CTETemp.[AP Document date],CTETemp.[PI Document No],CTETemp.[Payment Terms] ,CTETemp.[Credit Period],CTETemp.[Payment Document No],CTETemp.[Payment document date],CTETemp.[Payment amount] ,CTETemp.RowNo,CTETemp.[Location],CTETemp.[Days over due] order by CTETemp.[AP Document No],CTETemp.[AP Document date]"

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
                gv1.Columns("RowNo").IsVisible = False
                gv1.Columns("Vendor_Code").IsVisible = False

                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
                            End If
                            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If gv1.Rows.Count > 0 Then
                ''richa UDL/13/09/19-000999
                If gv1.CurrentColumn Is gv1.Columns("Payment Document No") Then
                    Dim DocNo As String = clsCommon.myCstr(e.Row.Cells.Item("Payment Document No").Value)
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, DocNo)
                ElseIf gv1.CurrentColumn Is gv1.Columns("AP Document No") Then
                    Dim DocNo As String = clsCommon.myCstr(e.Row.Cells.Item("AP Document No").Value)
                    ''richa UDL/24/09/19-001003 25 Sep,2019
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Invoice_Type,'') from tspl_vendor_invoice_head where document_no='" & DocNo & "'")), "VS") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, DocNo)
                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnAPInvoiceEntry, DocNo)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
                common.clsCommon.MyMessageBoxShow("No Data found")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
           

            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor Payment Details Report", gv1, arrHeader, "Vendor Payment Details Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data found")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub


End Class
