Imports common
'by vipin on 5 april 2013.
Public Class FrmDVAT30

    'update by vipin for pjv and gridExcel 26/10/2012
    'update by vipin 0n 31/10/2012 for detail data for vendor invoice head

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim refreshGrid As String = "F"


    Private Sub FrmDVAT30_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            print()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub

    Private Sub FrmDVAT30_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        reset()
        SetUserMgmtNew()

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.DVAT30)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport

        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "DVAT-30"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Public Sub reset()
        Try
            fromdate.Value = clsCommon.GETSERVERDATE()
            Todate.Value = clsCommon.GETSERVERDATE()
            chkVendorAll.IsChecked = True
            chkLocAll.IsChecked = True
            LoadVendor()
            LoadLocation()
            gv.DataSource = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER order by Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"

    End Sub
    Sub LoadLocation()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub



    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Function print() As String



        Try
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Vendor")
                Return Nothing
                'Exit Function
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Atleast Single Location Or select All ")
                Return Nothing
                'Exit Function
            End If
            Dim qry As String
            Dim str As String = "DVAT30 Report"
            'Dim head2 As String




            qry = "select * from(select   SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103),106),' ','-' ),4,10)as Mdate, month(convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)) as MD ,  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.Document_No, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as DocNo,convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)as Posting_Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as DocDate,TSPL_VENDOR_MASTER.Tin_No, " & _
               " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX1  )='V'  then TSPL_VENDOR_INVOICE_DETAIL.TAX1_Rate  " & _
               "   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX2  )='V'   then TSPL_VENDOR_INVOICE_DETAIL.TAX2_Rate   " & _
               "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX3  )='V'   then TSPL_VENDOR_INVOICE_DETAIL.TAX3_Rate " & _
               "  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX4  )='V'  then TSPL_VENDOR_INVOICE_DETAIL.TAX4_Rate   " & _
              "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX5  )='V'   then " & _
        "   TSPL_VENDOR_INVOICE_DETAIL.TAX5_Rate " & _
         "       else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX6  )='V'   then " & _
         "  TSPL_VENDOR_INVOICE_DETAIL.TAX6_Rate" & _
        "   else 0  end end end end end end) as TxRate, " & _
          "(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 1 else -1 end )* ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX1  )='V'  then TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX3  )='V'   then (TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount+TSPL_VENDOR_INVOICE_DETAIL.TAX1_Amt+TSPL_VENDOR_INVOICE_DETAIL.TAX2_Amt)  " & _
         "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX4  )='V'   then (TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount+TSPL_VENDOR_INVOICE_DETAIL.TAX1_Amt+TSPL_VENDOR_INVOICE_DETAIL.TAX2_Amt+TSPL_VENDOR_INVOICE_DETAIL.TAX3_Amt )   " & _
        "   else 0  end end end) as TxBaseAmt, " & _
        " (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 1 else -1 end )* ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX1  )='V'  then TSPL_VENDOR_INVOICE_DETAIL.TAX1_Amt  " & _
         "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX2  )='V'   then TSPL_VENDOR_INVOICE_DETAIL.TAX2_Amt   " & _
        "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX3  )='V'   then TSPL_VENDOR_INVOICE_DETAIL.TAX3_Amt " & _
        "  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX4  )='V'  then TSPL_VENDOR_INVOICE_DETAIL.TAX4_Amt   " & _
        "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX5  )='V'   then " & _
         "  TSPL_VENDOR_INVOICE_DETAIL.TAX5_Amt" & _
        "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX6  )='V'   then " & _
        "   TSPL_VENDOR_INVOICE_DETAIL.TAX6_Amt" & _
         "  else 0  end end end end end end) as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,case when TSPL_PJV_HEAD.PJV_No  is null then''  else TSPL_PJV_HEAD.PJV_No end as PJV_No   " & _
         "  from  TSPL_VENDOR_INVOICE_HEAD "
            qry += "left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL .Document_No "
            qry += "left outer join TSPL_GL_ACCOUNTS on TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code =TSPL_GL_ACCOUNTS .Account_Code " & _
         "  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
         "  LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code " & _
         " left outer join   TSPL_PJV_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PJV_HEAD.Invoice_No " & _
         "  where 2=2 "

            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            qry += "  and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "' and TSPL_VENDOR_INVOICE_HEAD.Posting_Date  is not null  )final where (TxRate<>0 or TxAmt<>0)  "



            If chkVendorSelect.IsChecked Then
                qry += " and Vendor_Code in  (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
            End If
            Dim final As String = "select Mdate as Month ,Tin_No as 'Sellers Tin',Vendor_Name as 'Seller Name' ,DocDate as 'Doc Date',posting_Date as 'Post Date',DocNo  as 'Doc No',PJV_No,'' as 'Import Out of India','' as 'Inter state Purchase or stock transfer','' as 'Purchase from exempted units','' as 'Total purchase including tax if any',SUM(TxAmt20) as 'AMT Taxable 20%',SUM(TxBaseAmt20) as 'Rate of Tax 20%',SUM(TxAmt12) as 'AMT Taxable 12.50%',SUM(TxBaseAmt12) as 'Rate of Tax 12.50%' , SUM(TxAmt5) as 'AMT Taxable 5%',SUM(TxBaseAmt5) as 'Rate of Tax 5%',SUM(TotalLocalpurchase) as 'Total Local purchase', SUM(TotalsaleTax) as 'Total Sale Tax','' as Transport,SUM(total) as 'Total purchase Including Tax' from(select Mdate ,MD ,Vendor_Code ,Vendor_Name ,DocNo,PJV_No ,posting_Date ,DocDate ,Tin_No ,TxRate ,"
            final += "(TxAmt12 +TxAmt20 +TxAmt5 ) as TotalLocalpurchase,TxAmt20 ,TxBaseAmt20 ,TxAmt12 ,TxBaseAmt12 ,TxAmt5 ,TxBaseAmt5 ,"
            final += "(TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5 ) as TotalsaleTax,"
            final += "(TxAmt12 +TxAmt20 +TxAmt5+TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5) as Total   from ( select Mdate ,MD ,Vendor_Code ,Vendor_Name ,DocNo ,posting_Date,DocDate  ,Tin_No, TxRate ,  sum(case when TxRate ='20' then TxBaseAmt  else 0 end) as TxAmt20, sum(case when TxRate='20' then TxAmt else 0 end) as TxBaseAmt20, sum(case when TxRate ='12.50' then TxBaseAmt  else 0 end) as TxAmt12, sum(case when TxRate='12.50' then TxAmt else 0 end) as TxBaseAmt12, sum(case when TxRate ='5' then TxBaseAmt  else 0 end) as TxAmt5, sum(case when TxRate='5' then TxAmt else 0 end) as TxBaseAmt5,PJV_No   from"
            final += "( " + qry + " ) zzzz group by Mdate ,MD ,Vendor_Code  ,Vendor_Name  ,DocNo ,posting_Date,DocDate  ,Tin_No ,TxRate,PJV_No   )qqqq   )LMNO group by Mdate ,MD ,Vendor_Code ,Vendor_Name ,DocNo ,posting_Date ,DocDate ,Tin_No ,PJV_No  order by MD  "


            qry += " order by md"

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(final)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gridformat()
            End If

            RadPageView1.SelectedPage = RadPageViewPage2


            'If refreshGrid = "F" AndAlso chkexcel.Checked = True Then

            '    head2 = "Summary of Purchase/Inward Branch Transfer Register"
            '    Dim arr As New List(Of String)()
            '    arr.Add("Annexure 2A")
            '    arr.Add("" + head2 + "(Month-Wise)")

            '    arr.Add(objCommonVar.CurrentCompanyName)
            '    'arr.Add("" + head1 + "  From:  " + fromdate.Value + "  To: " + Todate.Value + "")
            '    arr.Add("Summary of Purchase (As per DVAT-30)")
            '    Dim LocFilter As String
            '    Dim VendorFilter As String
            '    If cbgVendor.CheckedValue.Count > 0 Then
            '        VendorFilter = clsCommon.GetMulcallString(cbgVendor.CheckedValue)
            '        VendorFilter = VendorFilter.Replace("'", "")
            '        arr.Add("Vendor Filter :" + VendorFilter + "")
            '    End If
            '    If cbgLocation.CheckedValue.Count > 0 Then
            '        LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            '        LocFilter = LocFilter.Replace("'", "")
            '        arr.Add("Loc Filter :" + LocFilter + "")
            '    End If
            '    clsCommon.MyExportToExcel(str, gv, arr, "Summary of DVAT30")


            'End If
            'If refreshGrid = "F" AndAlso chkexcel.Checked = False Then

            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '        Throw New Exception("No Data found to Print")
            '    Else
            '        CommonServicesViewer.funreport(dt, "RptDVAT30", "DVAT-30")
            '    End If

            'End If

            Return qry
            refreshGrid = "F"

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

        Return Nothing

    End Function

    Sub printcrystal()
        Try
            Dim qry As String = print()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim frm As New frmCrystalReportViewer
                frm.funreport(CrystalReportFolder.CommonServices, dt, "RptDVAT30", "DVAT-30")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub


    Sub printdataExport(ByVal exporter As EnumExportTo)
        Try
            Dim qry As String = print()

            Dim str As String = "DVAT30 Report"




            Dim head2 As String = "Summary of Purchase/Inward Branch Transfer Register"
            Dim arr As New List(Of String)()
            arr.Add("Annexure 2A")
            arr.Add("" + head2 + "(Month-Wise)")

            arr.Add(objCommonVar.CurrentCompanyName)
            'arr.Add("" + head1 + "  From:  " + fromdate.Value + "  To: " + Todate.Value + "")
            arr.Add("Summary of Purchase (As per DVAT-30)")
            Dim LocFilter As String
            Dim VendorFilter As String
            If cbgVendor.CheckedValue.Count > 0 Then
                VendorFilter = clsCommon.GetMulcallString(cbgVendor.CheckedValue)
                VendorFilter = VendorFilter.Replace("'", "")
                arr.Add("Vendor Filter :" + VendorFilter + "")
            End If
            If cbgLocation.CheckedValue.Count > 0 Then
                LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LocFilter = LocFilter.Replace("'", "")
                arr.Add("Loc Filter :" + LocFilter + "")
            End If
            'clsCommon.MyExportToExcel(str, gv, arr, "Summary of DVAT30")

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel(str, gv, arr, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF(str, gv, arr, Me.Text, True)
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub
    Public Sub gridformat()
        Try

            gv.MasterTemplate.SummaryRowsBottom.Clear()


            Dim summaryRowItem As New GridViewSummaryRowItem()

            gv.AllowAddNewRow = False

            gv.Columns("Month").IsVisible = True
            gv.Columns("Month").Width = 100
            gv.Columns("Month").HeaderText = "Month"

            gv.Columns("Sellers Tin").IsVisible = True
            gv.Columns("Sellers Tin").Width = 100
            gv.Columns("Sellers Tin").HeaderText = "Sellers Tin"

            gv.Columns("Seller Name").IsVisible = True
            gv.Columns("Seller Name").Width = 200
            gv.Columns("Seller Name").HeaderText = "Seller Name"

            gv.Columns("Doc Date").IsVisible = True
            gv.Columns("Doc Date").Width = 100
            gv.Columns("Doc Date").HeaderText = "Doc Date"

            gv.Columns("Post Date").IsVisible = True
            gv.Columns("Post Date").Width = 100
            gv.Columns("Post Date").HeaderText = "Post Date"

            gv.Columns("Doc No").IsVisible = True
            gv.Columns("Doc No").Width = 100
            gv.Columns("Doc No").HeaderText = "Doc No"


            gv.Columns("PJV_No").IsVisible = True
            gv.Columns("PJV_No").Width = 100
            gv.Columns("PJV_No").HeaderText = "PJV No"


            gv.Columns("Import Out of India").IsVisible = True
            gv.Columns("Import Out of India").Width = 150
            gv.Columns("Import Out of India").HeaderText = "Import Out of India"

            gv.Columns("Inter state Purchase or Stock transfer").IsVisible = True
            gv.Columns("Inter state Purchase or Stock transfer").Width = 250
            gv.Columns("Inter state Purchase or Stock transfer").HeaderText = "Inter state Purchase or Stock transfer"



            gv.Columns("Purchase from exempted units").IsVisible = True
            gv.Columns("Purchase from exempted units").Width = 150
            gv.Columns("Purchase from exempted units").HeaderText = "Purchase from exempted units"


            gv.Columns("Total Purchase including Tax if any").IsVisible = True
            gv.Columns("Total Purchase including Tax if any").Width = 150
            gv.Columns("Total Purchase including Tax if any").HeaderText = "Total Purchase including Tax if any"



            gv.Columns("AMT Taxable 20%").IsVisible = True
            gv.Columns("AMT Taxable 20%").Width = 100
            gv.Columns("AMT Taxable 20%").HeaderText = "AMT Taxable 20%"

            gv.Columns("Rate of Tax 20%").IsVisible = True
            gv.Columns("Rate of Tax 20%").Width = 100
            gv.Columns("Rate of Tax 20%").HeaderText = "Rate of Tax 20%"

            gv.Columns("AMT Taxable 12.50%").IsVisible = True
            gv.Columns("AMT Taxable 12.50%").Width = 100
            gv.Columns("AMT Taxable 12.50%").HeaderText = "AMT Taxable 12.50%"

            gv.Columns("Rate of Tax 12.50%").IsVisible = True
            gv.Columns("Rate of Tax 12.50%").Width = 100
            gv.Columns("Rate of Tax 12.50%").HeaderText = "Rate of Tax 12.50%"

            gv.Columns("AMT Taxable 5%").IsVisible = True
            gv.Columns("AMT Taxable 5%").Width = 100
            gv.Columns("AMT Taxable 5%").HeaderText = "AMT Taxable 5%"

            gv.Columns("Rate of Tax 5%").IsVisible = True
            gv.Columns("Rate of Tax 5%").Width = 100
            gv.Columns("Rate of Tax 5%").HeaderText = "Rate of Tax 5%"

            gv.Columns("Total Local purchase").IsVisible = True
            gv.Columns("Total Local purchase").Width = 100
            gv.Columns("Total Local purchase").HeaderText = "Total Local purchase"

            gv.Columns("Total Sale Tax").IsVisible = True
            gv.Columns("Total Sale Tax").Width = 100
            gv.Columns("Total Sale Tax").HeaderText = "Total Sale Tax"

            gv.Columns("Transport").IsVisible = True
            gv.Columns("Transport").Width = 100
            gv.Columns("Transport").HeaderText = "Transport"

            gv.Columns("Total purchase Including Tax").IsVisible = True
            gv.Columns("Total purchase Including Tax").Width = 100
            gv.Columns("Total purchase Including Tax").HeaderText = "Total purchase Including Tax"



            Dim SumAmtTAx20 As New GridViewSummaryItem("AMT Taxable 20%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumAmtTAx20)
            Dim SumRate20 As New GridViewSummaryItem("Rate of Tax 20%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumRate20)
            Dim SumAmtTAx12 As New GridViewSummaryItem("AMT Taxable 12.50%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumAmtTAx12)
            Dim SumRate12 As New GridViewSummaryItem("Rate of Tax 12.50%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumRate12)

            Dim SumAmtTAx5 As New GridViewSummaryItem("AMT Taxable 5%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumAmtTAx5)
            Dim SumRate5 As New GridViewSummaryItem("Rate of Tax 5%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumRate5)
            Dim SumPurchaseTAx As New GridViewSummaryItem("Total Local purchase", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumPurchaseTAx)
            Dim SumSaleTAx As New GridViewSummaryItem("Total Sale Tax", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumSaleTAx)
            Dim SumTotal As New GridViewSummaryItem("Total purchase Including Tax", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTotal)


            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        refreshGrid = "T"
        print()
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub



    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        printcrystal()
    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub


   

    Private Sub btnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        printdataExport(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        printdataExport(EnumExportTo.PDF)
    End Sub
End Class
