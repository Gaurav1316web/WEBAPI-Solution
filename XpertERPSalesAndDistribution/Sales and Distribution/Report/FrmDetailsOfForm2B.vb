
Imports common
Imports XpertERPEngine

Public Class FrmDetailsOfForm2B

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()





    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Public Sub reset()
        Try
            fromdate.Value = clsCommon.GETSERVERDATE()
            Todate.Value = clsCommon.GETSERVERDATE()
            LoadCustomer()
            LoadLocation()
            chkallcustomer.IsChecked = True
            chkLocAll.IsChecked = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"

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

    Private Sub FrmDVAT31_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FrmDVAT31_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        reset()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDetailsOfForm2B)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub



    Private Sub chkallcustomer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkallcustomer.ToggleStateChanged
        cbgCustomer.Enabled = Not chkallcustomer.IsChecked
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub
    ''    Sub print()
    ''        Try
    ''            If chkselectcustomer.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
    ''                common.clsCommon.MyMessageBoxShow("Please Select Atleast One customer")
    ''                Return
    ''            End If
    ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
    ''                common.clsCommon.MyMessageBoxShow("Please select Atleast Single Location Or select All ")
    ''                Return
    ''            End If
    ''            Dim qry As String

    ''            qry = "  select max (FDaTe) as FDaTe,max(todate) as todate,Mdate,MD,cust_Code,cust_Name,Tin_No,TxRate,SUM ( TxBaseAmt) as TxBaseAmt,sum (Total_net_Amt) as Total_net_Amt, sum (TxAmt) as TxAmt ,MAX( compTin) as compTin  ,max(cform) as cform from(  select * from ( select * from ( " & _
    ''            "  select '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103)) as MD,TSPL_SCRAPINVOICE_HEAD.cust_Code,TSPL_SCRAPINVOICE_HEAD.cust_Name,TSPL_SCRAPINVOICE_HEAD.invoice_No as DocNo,convert(varchar(11),TSPL_SCRAPINVOICE_HEAD.posting_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ''         "  ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Rate   " & _
    ''         "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='V'   then TSPL_SCRAPINVOICE_HEAD.TAX2_Rate   " & _
    ''       "   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='V'   then TSPL_SCRAPINVOICE_HEAD.TAX3_Rate " & _
    ''  "  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX4_Rate   " & _
    '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='V'   then " & _
    ''"  TSPL_SCRAPINVOICE_HEAD.TAX5_Rate " & _
    '' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='V'   then " & _
    '' "  TSPL_SCRAPINVOICE_HEAD.TAX6_Rate " & _
    ''"  else 0  end end end end end end) as TxRate,  TSPL_SCRAPINVOICE_HEAD.Amount_Less_Discount as Total_net_Amt , " & _
    ''"   ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Base_Amt   " & _
    '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='V'   then TSPL_SCRAPINVOICE_HEAD.Tax2_Base_Amt   " & _
    '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='V'   then TSPL_SCRAPINVOICE_HEAD.Tax3_Base_Amt   " & _
    '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='V'  then TSPL_SCRAPINVOICE_HEAD.Tax4_Base_Amt   " & _
    ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='V'   then " & _
    ''"  TSPL_SCRAPINVOICE_HEAD.Tax5_Base_Amt   " & _
    ''"  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='V'   then " & _
    '' "  TSPL_SCRAPINVOICE_HEAD.Tax6_Base_Amt   " & _
    ''"  else 0  end end end end end end) as TxBaseAmt, " & _
    '' "  ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Amt   " & _
    ''" else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='V'   then TSPL_SCRAPINVOICE_HEAD.TAX2_Amt   " & _
    ''" else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='V'   then TSPL_SCRAPINVOICE_HEAD.TAX3_Amt " & _
    ''"  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX4_Amt  " & _
    ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='V'   then " & _
    '' " TSPL_SCRAPINVOICE_HEAD.TAX5_Amt " & _
    ''"  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='V'   then " & _
    '' "  TSPL_SCRAPINVOICE_HEAD.TAX6_Amt " & _
    ''"  else 0  end end end end end end) as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1   ,TSPL_COMPANY_MASTER.Tin_No as compTin  ,'' as cform " & _
    ''"     from TSPL_SCRAPINVOICE_HEAD " & _
    ''"    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code " & _
    ''"    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code " & _
    '' "  LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SCRAPINVOICE_HEAD.Comp_Code " & _
    ''"  where 2=2 "
    ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    ''                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    ''            End If
    ''            qry += " ) finalSC where   convert(date, posting_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' and posting_Date<='" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' " & _
    '' "   union all " & _
    '' "  select * from  " & _
    ''" (select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,substring( convert(varchar(11),convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as MD,TSPL_SALE_INVOICE_HEAD.cust_Code,TSPL_SALE_INVOICE_HEAD.cust_Name,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No as DocNo,convert(varchar(11),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ''"( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Rate  " & _
    ''"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Rate  " & _
    '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Rate " & _
    '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Rate   " & _
    '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    ''  "          TSPL_SALE_INVOICE_DETAIL.TAX5_Rate " & _
    ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    ''   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Rate " & _
    '' " else 0  end end end end end end) as TxRate, TSPL_SALE_INVOICE_DETAIL.Total_net_Amt , " & _
    '' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt   " & _
    ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.Tax2_Assessable_Amt   " & _
    ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.Tax3_Assessable_Amt  " & _
    ''  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.Tax4_Assessable_Amt   " & _
    ''  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    ''   "         TSPL_SALE_INVOICE_DETAIL.Tax5_Assessable_Amt " & _
    '' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    ''  "          TSPL_SALE_INVOICE_DETAIL.Tax6_Assessable_Amt " & _
    ''  " else 0  end end end end end end) * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxBaseAmt, " & _
    '' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Amt  " & _
    ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Amt   " & _
    ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Amt " & _
    ''  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Amt   " & _
    ''  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    ''   "         TSPL_SALE_INVOICE_DETAIL.TAX5_Amt " & _
    ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    ''   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Amt " & _
    ''   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Tin_No as compTin   ,TSPL_SALE_INVOICE_HEAD.Against_C_Form as cform" & _
    ''   "         from TSPL_SALE_INVOICE_DETAIL " & _
    ''  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
    ''" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
    ''" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
    ''  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
    ''  " where 2=2  "
    ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    ''                qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    ''            End If
    ''            qry += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' ) finalSI  " & _
    ''           " where TxRate <> 0 )abc where 2=2 and TxRate <> 0  "


    ''            If chkselectcustomer.IsChecked Then
    ''                qry += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
    ''            End If
    ''            qry += ")  main group by   cust_Code,cust_Name,Mdate,MD,Tin_No,TxRate order by MD "






    ''            '----------------------For cst tax------------------------------------------------------------------




    ''            Dim qry1 As String

    ''            qry1 = "  select max (FDaTe) as FDaTe,max(todate) as todate,Mdate,cust_Code,cust_Name,Tin_No,TxRate,SUM ( TxBaseAmt) as TxBaseAmt,sum (Total_net_Amt) as Total_net_Amt, sum (TxAmt) as TxAmt ,MAX( compTin) as compTin  ,max(cform) as cform from(  select * from ( select * from ( " & _
    ''            "  select '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103)) as MD,TSPL_SCRAPINVOICE_HEAD.cust_Code,TSPL_SCRAPINVOICE_HEAD.cust_Name,TSPL_SCRAPINVOICE_HEAD.invoice_No as DocNo,convert(varchar(11),TSPL_SCRAPINVOICE_HEAD.posting_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ''         "  ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Rate   " & _
    ''         "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='c'   then TSPL_SCRAPINVOICE_HEAD.TAX2_Rate   " & _
    ''       "   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='c'   then TSPL_SCRAPINVOICE_HEAD.TAX3_Rate " & _
    ''  "  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX4_Rate   " & _
    '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='c'   then " & _
    ''"  TSPL_SCRAPINVOICE_HEAD.TAX5_Rate " & _
    '' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='c'   then " & _
    '' "  TSPL_SCRAPINVOICE_HEAD.TAX6_Rate " & _
    ''"  else 0  end end end end end end) as TxRate,  TSPL_SCRAPINVOICE_HEAD.Amount_Less_Discount as Total_net_Amt , " & _
    ''"   ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Base_Amt   " & _
    '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='c'   then TSPL_SCRAPINVOICE_HEAD.Tax2_Base_Amt   " & _
    '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='c'   then TSPL_SCRAPINVOICE_HEAD.Tax3_Base_Amt   " & _
    '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='c'  then TSPL_SCRAPINVOICE_HEAD.Tax4_Base_Amt   " & _
    ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='c'   then " & _
    ''"  TSPL_SCRAPINVOICE_HEAD.Tax5_Base_Amt   " & _
    ''"  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='c'   then " & _
    '' "  TSPL_SCRAPINVOICE_HEAD.Tax6_Base_Amt   " & _
    ''"  else 0  end end end end end end) as TxBaseAmt, " & _
    '' "  ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Amt   " & _
    ''" else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='c'   then TSPL_SCRAPINVOICE_HEAD.TAX2_Amt   " & _
    ''" else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='c'   then TSPL_SCRAPINVOICE_HEAD.TAX3_Amt " & _
    ''"  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX4_Amt  " & _
    ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='c'   then " & _
    '' " TSPL_SCRAPINVOICE_HEAD.TAX5_Amt " & _
    ''"  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='c'   then " & _
    '' "  TSPL_SCRAPINVOICE_HEAD.TAX6_Amt " & _
    ''"  else 0  end end end end end end) as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1   ,TSPL_COMPANY_MASTER.Tin_No as compTin  ,'' as cform " & _
    ''"     from TSPL_SCRAPINVOICE_HEAD " & _
    ''"    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code " & _
    ''"    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code " & _
    '' "  LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SCRAPINVOICE_HEAD.Comp_Code " & _
    ''"  where 2=2 "
    ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    ''                qry1 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    ''            End If
    ''            qry1 += " ) finalSC where    posting_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' and posting_Date<='" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' " & _
    '' "   union all " & _
    '' "  select * from  " & _
    ''" (select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,substring( convert(varchar(11),convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as MD,TSPL_SALE_INVOICE_HEAD.cust_Code,TSPL_SALE_INVOICE_HEAD.cust_Name,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No as DocNo,convert(varchar(11),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ''"( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Rate  " & _
    ''"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Rate  " & _
    '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Rate " & _
    '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Rate   " & _
    '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
    ''  "          TSPL_SALE_INVOICE_DETAIL.TAX5_Rate " & _
    ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
    ''   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Rate " & _
    '' " else 0  end end end end end end) as TxRate, TSPL_SALE_INVOICE_DETAIL.Total_net_Amt , " & _
    '' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt   " & _
    ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.Tax2_Assessable_Amt   " & _
    ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.Tax3_Assessable_Amt  " & _
    ''  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.Tax4_Assessable_Amt   " & _
    ''  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
    ''   "         TSPL_SALE_INVOICE_DETAIL.Tax5_Assessable_Amt " & _
    '' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
    ''  "          TSPL_SALE_INVOICE_DETAIL.Tax6_Assessable_Amt " & _
    ''  " else 0  end end end end end end) * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxBaseAmt, " & _
    '' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Amt  " & _
    ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Amt   " & _
    ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Amt " & _
    ''  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Amt   " & _
    ''  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
    ''   "         TSPL_SALE_INVOICE_DETAIL.TAX5_Amt " & _
    ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
    ''   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Amt " & _
    ''   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Tin_No as compTin   ,TSPL_SALE_INVOICE_HEAD.Against_C_Form as cform" & _
    ''   "         from TSPL_SALE_INVOICE_DETAIL " & _
    ''  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
    ''" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
    ''" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
    ''  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
    ''  " where 2=2  "
    ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    ''                qry1 += "and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    ''            End If
    ''            qry1 += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' ) finalSI  " & _
    ''           " where TxRate <> 0 )abc where 2=2 and TxRate <> 0  "


    ''            If chkselectcustomer.IsChecked Then
    ''                qry1 += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
    ''            End If
    ''            qry1 += ")  main group by   cust_Code,cust_Name,Mdate,MD,Tin_No,TxRate  order by MD"

    ''            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
    ''            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
    ''                Dim frm As New FrmSalerReport()

    ''                frm.funreport(qry1, "DetailsOfForm2BCST", "Details of Form 2B")
    ''            Else
    ''                Dim frm As New FrmSalerReport()
    ''                ' frm.funSubreport(qry1, qry, "DetailsOfForm2B", "Details of Form 2B", "DetailsOfForm2BVAT.rpt")
    ''                frm.funSubreport(qry, qry1, "DetailsOfForm2BVAT", "Details of Form 2B", "DetailsOfForm2B.rpt")
    ''                'CommonServicesViewer.funreport(dt1, "DetailsOfForm2B", "Details of Form 2B")
    ''            End If



    ''            '---------------------------------------------------------------------------------------------------

    ''            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    ''            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    ''            '    Throw New Exception("No Data found to Print")
    ''            'Else
    ''            '    CommonServicesViewer.funreport(dt, "DetailsOfForm2BVAT", "Details of Form 2B Vat")
    ''            'End If








    ''        Catch ex As Exception
    ''            common.clsCommon.MyMessageBoxShow(ex.Message)
    ''        End Try
    ''    End Sub



    '    Sub print()
    '        Try
    '            If chkselectcustomer.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
    '                common.clsCommon.MyMessageBoxShow("Please Select Atleast One customer")
    '                Return
    '            End If
    '            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
    '                common.clsCommon.MyMessageBoxShow("Please select Atleast Single Location Or select All ")
    '                Return
    '            End If
    '            Dim qry As String

    '            qry = "  select 'vat' as type,max (FDaTe) as FDaTe,max(todate) as todate,Mdate,MD,cust_Code,cust_Name,Tin_No,TxRate,SUM ( TxBaseAmt) as TxBaseAmt,sum (Total_net_Amt) as Total_net_Amt, sum (TxAmt) as TxAmt ,MAX( compTin) as compTin  ,max(cform) as cform from(  select * from (  " & _
    '            " Select FDaTe,todate,Mdate,MD,cust_code,cust_name,DocNo,posting_Date,Tin_No,TxRate,Total_net_Amt,TxBaseAmt,TxAmt,Comp_Name,Add1,compTin,cform from(select '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_Customer_Invoice_Head.posting_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_Customer_Invoice_Head.posting_Date,103)) as MD,TSPL_Customer_Invoice_Head.Customer_Code as cust_code,TSPL_Customer_Invoice_Head.Customer_Name as cust_name,TSPL_Customer_Invoice_Head.Document_No as DocNo,convert(date,TSPL_Customer_Invoice_Head.posting_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No ,   " & _
    '" ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.TAX1_Rate  " & _
    '" else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.TAX2_Rate " & _
    ' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.TAX3_Rate " & _
    ' "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.TAX4_Rate " & _
    ' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then   TSPL_Customer_Invoice_Head.TAX5_Rate       else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then   TSPL_Customer_Invoice_Head.TAX6_Rate  " & _
    ' "  else 0  end end end end end end) as TxRate, " & _
    ' "   TSPL_Customer_Invoice_Head.Amount_Less_Discount as Total_net_Amt , " & _
    '   "  ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.Tax1_BAmount    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.Tax2_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.Tax3_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.Tax4_BAmount     else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then   TSPL_Customer_Invoice_Head.Tax5_BAmount     else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then   TSPL_Customer_Invoice_Head.Tax6_BAmount     else 0  end end end end end end) as TxBaseAmt, " & _
    '   " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.TAX1_Amt " & _
    '   "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.TAX2_Amt    else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.TAX3_Amt " & _
    '  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.TAX4_Amt    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then  TSPL_Customer_Invoice_Head.TAX5_Amt " & _
    ' "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then   TSPL_Customer_Invoice_Head.TAX6_Amt " & _
    '   "  else 0  end end end end end end) as TxAmt," & _
    '   "  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1   ,TSPL_COMPANY_MASTER.Tin_No as compTin  ,'' as cform ,   (select substring(TSPL_Customer_Invoice_Detail.GL_Account_Code,len(TSPL_Customer_Invoice_Detail.GL_Account_Code)-2,4) from TSPL_Customer_Invoice_Detail where TSPL_Customer_Invoice_Detail.Document_No=TSPL_Customer_Invoice_Head.Document_No and SNo='1' )as loc    from TSPL_Customer_Invoice_Head " & _
    '" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code  " & _
    ' " LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Customer_Invoice_Head.Comp_Code   where 2=2 AND TSPL_Customer_Invoice_Head.Status='1'  "

    '            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '                qry += " and loc in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '            End If
    '            qry += " ) finalAR where   convert(date, posting_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and posting_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' " & _
    '            "   union all " & _
    '  "   select * from  (  select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103),105),4,7 )as Mdate ,month(convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)) as MD," & _
    '      " TSPL_SALE_RETURN_HEAD.cust_Code,TSPL_SALE_RETURN_HEAD.cust_Name,TSPL_SALE_RETURN_DETAIL.Sale_Return_No as DocNo,convert(varchar(11),TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ' " ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Rate  " & _
    '   " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX2_Rate   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX3_Rate " & _
    '  "   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX4_Rate  " & _
    '  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then  TSPL_SALE_RETURN_DETAIL.TAX5_Rate " & _
    '   " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then   TSPL_SALE_RETURN_DETAIL.TAX6_Rate " & _
    '   "  else 0  end end end end end end) as TxRate, TSPL_SALE_RETURN_DETAIL.Total_net_Amt , " & _
    '   " ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Assessable_Amt       else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax2_Assessable_Amt    else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax3_Assessable_Amt   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.Tax4_Assessable_Amt    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then          TSPL_SALE_RETURN_DETAIL.Tax5_Assessable_Amt  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then           TSPL_SALE_RETURN_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_DETAIL.Return_Qty as TxBaseAmt, " & _
    '   " ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Amt  " & _
    '   "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX2_Amt " & _
    '   "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX3_Amt " & _
    '   "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX4_Amt   " & _
    '   "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then   TSPL_SALE_RETURN_DETAIL.TAX5_Amt " & _
    '   "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then      TSPL_SALE_RETURN_DETAIL.TAX6_Amt  " & _
    '   "  else 0  end end end end end end)* TSPL_SALE_RETURN_DETAIL.Return_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,          TSPL_COMPANY_MASTER.Tin_No as compTin   ,'' as cform   " & _
    '   "   from TSPL_SALE_RETURN_DETAIL  left outer join TSPL_SALE_RETURN_HEAD  on TSPL_SALE_RETURN_DETAIL.Sale_Return_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_HEAD.Cust_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_HEAD.Comp_Code  where 2=2  AND TSPL_SALE_RETURN_HEAD.Is_Post='Y' " & _
    '"   and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "'  "
    '            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '            qry += " and  TSPL_SALE_RETURN_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
    '            End If
    '            qry += " ) finalSR  " & _
    '  "   union all " & _
    '"  select * from   (  select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),105),4,7 )as Mdate ,month(convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)) as MD, " & _
    '     "  TSPL_SALE_RETURN_INTER_HEAD.cust_Code,TSPL_SALE_RETURN_INTER_HEAD.cust_Name,TSPL_SALE_RETURN_INTER_DETAIL.Document_No as DocNo,convert(varchar(11),TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ' " ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Rate  " & _
    '  "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Rate   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Rate " & _
    '  "   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Rate   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then  TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Rate " & _
    '  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then   TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Rate  else 0  end end end end end end) as TxRate, TSPL_SALE_RETURN_INTER_DETAIL.Total_net_Amt , " & _
    '  "  ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Assessable_Amt       else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax2_Assessable_Amt    else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax3_Assessable_Amt   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.Tax4_Assessable_Amt    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then          TSPL_SALE_RETURN_INTER_DETAIL.Tax5_Assessable_Amt  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then           TSPL_SALE_RETURN_INTER_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_INTER_DETAIL.Qty as TxBaseAmt, " & _
    ' " ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt  " & _
    ' " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt  " & _
    '"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt " & _
    '"  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt " & _
    '"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then   TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then      TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Amt   else 0  end end end end end end)* TSPL_SALE_RETURN_INTER_DETAIL.Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,          TSPL_COMPANY_MASTER.Tin_No as compTin   ,'' as cform  " & _
    '  "    from TSPL_SALE_RETURN_INTER_DETAIL  left outer join TSPL_SALE_RETURN_INTER_HEAD  on TSPL_SALE_RETURN_INTER_DETAIL.Document_No=TSPL_SALE_RETURN_INTER_HEAD.Document_No  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_INTER_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_INTER_HEAD.Comp_Code  where 2=2  AND TSPL_SALE_RETURN_INTER_HEAD.Is_Post='1' " & _
    '"   and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "'  "
    '            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '                qry += " and  TSPL_SALE_RETURN_INTER_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
    '            End If
    '            qry += " ) finalSRI  " & _
    '   "   union all " & _
    ' "  select * from  " & _
    '" (select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,substring( convert(varchar(11),convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as MD,TSPL_SALE_INVOICE_HEAD.cust_Code,TSPL_SALE_INVOICE_HEAD.cust_Name,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No as DocNo,convert(varchar(11),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    '"( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Rate  " & _
    '"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Rate  " & _
    ' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Rate " & _
    ' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Rate   " & _
    ' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    '  "          TSPL_SALE_INVOICE_DETAIL.TAX5_Rate " & _
    '  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    '   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Rate " & _
    ' " else 0  end end end end end end) as TxRate, TSPL_SALE_INVOICE_DETAIL.Total_net_Amt , " & _
    ' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt   " & _
    '  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.Tax2_Assessable_Amt   " & _
    '  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.Tax3_Assessable_Amt  " & _
    '  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.Tax4_Assessable_Amt   " & _
    '  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    '   "         TSPL_SALE_INVOICE_DETAIL.Tax5_Assessable_Amt " & _
    ' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    '  "          TSPL_SALE_INVOICE_DETAIL.Tax6_Assessable_Amt " & _
    '  " else 0  end end end end end end) * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxBaseAmt, " & _
    ' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Amt  " & _
    '  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Amt   " & _
    '  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Amt " & _
    '  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Amt   " & _
    '  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    '   "         TSPL_SALE_INVOICE_DETAIL.TAX5_Amt " & _
    '  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    '   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Amt " & _
    '   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Tin_No as compTin   ,TSPL_SALE_INVOICE_HEAD.Against_C_Form as cform" & _
    '   "         from TSPL_SALE_INVOICE_DETAIL " & _
    '  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
    '" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
    '" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
    '  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
    '  " where 2=2  AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
    '            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '                qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '            End If
    '            qry += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' ) finalSI  " & _
    '           " where TxRate <> 0 )abc where 2=2 and TxRate <> 0  "


    '            If chkselectcustomer.IsChecked Then
    '                qry += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
    '            End If
    '            qry += ")  main group by   cust_Code,cust_Name,Mdate,MD,Tin_No,TxRate "






    '            '----------------------For cst tax------------------------------------------------------------------




    '            Dim qry1 As String

    '            qry1 = "  select 'cst' as type,max (FDaTe) as FDaTe,max(todate) as todate,Mdate,MD,cust_Code,cust_Name,Tin_No,TxRate,SUM ( TxBaseAmt) as TxBaseAmt,sum (Total_net_Amt) as Total_net_Amt, sum (TxAmt) as TxAmt ,MAX( compTin) as compTin  ,max(cform) as cform from(  select * from  ( " & _
    '           " Select FDaTe,todate,Mdate,MD,cust_code,cust_name,DocNo,posting_Date,Tin_No,TxRate,Total_net_Amt,TxBaseAmt,TxAmt,Comp_Name,Add1,compTin,cform from(select '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_Customer_Invoice_Head.posting_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_Customer_Invoice_Head.posting_Date,103)) as MD,TSPL_Customer_Invoice_Head.Customer_Code as cust_code,TSPL_Customer_Invoice_Head.Customer_Name as cust_name,TSPL_Customer_Invoice_Head.Document_No as DocNo,convert(date,TSPL_Customer_Invoice_Head.posting_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No ,   " & _
    '" ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='c'  then TSPL_Customer_Invoice_Head.TAX1_Rate  " & _
    '" else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='c'   then TSPL_Customer_Invoice_Head.TAX2_Rate " & _
    ' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='c'   then TSPL_Customer_Invoice_Head.TAX3_Rate " & _
    ' "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='c'  then TSPL_Customer_Invoice_Head.TAX4_Rate " & _
    ' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='c'   then   TSPL_Customer_Invoice_Head.TAX5_Rate       else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='c'   then   TSPL_Customer_Invoice_Head.TAX6_Rate  " & _
    ' "  else 0  end end end end end end) as TxRate, " & _
    ' "   TSPL_Customer_Invoice_Head.Amount_Less_Discount as Total_net_Amt , " & _
    '   "  ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='c'  then TSPL_Customer_Invoice_Head.Tax1_BAmount    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='c'   then TSPL_Customer_Invoice_Head.Tax2_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='c'   then TSPL_Customer_Invoice_Head.Tax3_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='c'  then TSPL_Customer_Invoice_Head.Tax4_BAmount     else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='c'   then   TSPL_Customer_Invoice_Head.Tax5_BAmount     else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='c'   then   TSPL_Customer_Invoice_Head.Tax6_BAmount     else 0  end end end end end end) as TxBaseAmt, " & _
    '   " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='c'  then TSPL_Customer_Invoice_Head.TAX1_Amt " & _
    '   "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='v'   then TSPL_Customer_Invoice_Head.TAX2_Amt    else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='c'   then TSPL_Customer_Invoice_Head.TAX3_Amt " & _
    '  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='c'  then TSPL_Customer_Invoice_Head.TAX4_Amt    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='c'   then  TSPL_Customer_Invoice_Head.TAX5_Amt " & _
    ' "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='c'   then   TSPL_Customer_Invoice_Head.TAX6_Amt " & _
    '   "  else 0  end end end end end end) as TxAmt," & _
    '   "  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1   ,TSPL_COMPANY_MASTER.Tin_No as compTin  ,'' as cform ,   (select substring(TSPL_Customer_Invoice_Detail.GL_Account_Code,len(TSPL_Customer_Invoice_Detail.GL_Account_Code)-2,4) from TSPL_Customer_Invoice_Detail where TSPL_Customer_Invoice_Detail.Document_No=TSPL_Customer_Invoice_Head.Document_No and SNo='1' )as loc    from TSPL_Customer_Invoice_Head " & _
    '" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code  " & _
    ' " LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Customer_Invoice_Head.Comp_Code   where 2=2 AND TSPL_Customer_Invoice_Head.Status='1'  "

    '            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '                qry1 += " and loc in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '            End If
    '            qry1 += " ) finalARC where   convert(date, posting_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and posting_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' " & _
    '            "   union all " & _
    '  "   select * from  (  select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103),105),4,7 )as Mdate ,month(convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)) as MD," & _
    '      " TSPL_SALE_RETURN_HEAD.cust_Code,TSPL_SALE_RETURN_HEAD.cust_Name,TSPL_SALE_RETURN_DETAIL.Sale_Return_No as DocNo,convert(varchar(11),TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ' " ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX1_Rate  " & _
    '   " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_DETAIL.TAX2_Rate   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_DETAIL.TAX3_Rate " & _
    '  "   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX4_Rate  " & _
    '  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='c'   then  TSPL_SALE_RETURN_DETAIL.TAX5_Rate " & _
    '   " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='c'   then   TSPL_SALE_RETURN_DETAIL.TAX6_Rate " & _
    '   "  else 0  end end end end end end) as TxRate, TSPL_SALE_RETURN_DETAIL.Total_net_Amt , " & _
    '   " ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX1_Assessable_Amt       else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_DETAIL.Tax2_Assessable_Amt    else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_DETAIL.Tax3_Assessable_Amt   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_DETAIL.Tax4_Assessable_Amt    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='c'   then          TSPL_SALE_RETURN_DETAIL.Tax5_Assessable_Amt  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='c'   then           TSPL_SALE_RETURN_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_DETAIL.Return_Qty as TxBaseAmt, " & _
    '   " ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX1_Amt  " & _
    '   "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_DETAIL.TAX2_Amt " & _
    '   "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_DETAIL.TAX3_Amt " & _
    '   "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX4_Amt   " & _
    '   "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='c'   then   TSPL_SALE_RETURN_DETAIL.TAX5_Amt " & _
    '   "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='c'   then      TSPL_SALE_RETURN_DETAIL.TAX6_Amt  " & _
    '   "  else 0  end end end end end end)* TSPL_SALE_RETURN_DETAIL.Return_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,          TSPL_COMPANY_MASTER.Tin_No as compTin   ,'' as cform   " & _
    '   "   from TSPL_SALE_RETURN_DETAIL  left outer join TSPL_SALE_RETURN_HEAD  on TSPL_SALE_RETURN_DETAIL.Sale_Return_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_HEAD.Cust_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_HEAD.Comp_Code  where 2=2  AND TSPL_SALE_RETURN_HEAD.Is_Post='Y' " & _
    '"   and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "'  "
    '            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '                qry1 += " and  TSPL_SALE_RETURN_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
    '            End If
    '            qry1 += " ) finalSRC  " & _
    '  "   union all " & _
    '"  select * from   (  select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),105),4,7 )as Mdate ,month(convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)) as MD, " & _
    '     "  TSPL_SALE_RETURN_INTER_HEAD.cust_Code,TSPL_SALE_RETURN_INTER_HEAD.cust_Name,TSPL_SALE_RETURN_INTER_DETAIL.Document_No as DocNo,convert(varchar(11),TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ' " ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Rate  " & _
    '  "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Rate   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Rate " & _
    '  "   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Rate   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='c'   then  TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Rate " & _
    '  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='c'   then   TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Rate  else 0  end end end end end end) as TxRate, TSPL_SALE_RETURN_INTER_DETAIL.Total_net_Amt , " & _
    '  "  ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Assessable_Amt       else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax2_Assessable_Amt    else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax3_Assessable_Amt   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.Tax4_Assessable_Amt    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='c'   then          TSPL_SALE_RETURN_INTER_DETAIL.Tax5_Assessable_Amt  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='c'   then           TSPL_SALE_RETURN_INTER_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_INTER_DETAIL.Qty as TxBaseAmt, " & _
    ' " ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt  " & _
    ' " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt  " & _
    '"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt " & _
    '"  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt " & _
    '"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='c'   then   TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='c'   then      TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Amt   else 0  end end end end end end)* TSPL_SALE_RETURN_INTER_DETAIL.Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,          TSPL_COMPANY_MASTER.Tin_No as compTin   ,'' as cform  " & _
    '  "    from TSPL_SALE_RETURN_INTER_DETAIL  left outer join TSPL_SALE_RETURN_INTER_HEAD  on TSPL_SALE_RETURN_INTER_DETAIL.Document_No=TSPL_SALE_RETURN_INTER_HEAD.Document_No  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_INTER_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_INTER_HEAD.Comp_Code  where 2=2  AND TSPL_SALE_RETURN_INTER_HEAD.Is_Post='1' " & _
    '"   and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "'  "
    '            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '                qry1 += " and  TSPL_SALE_RETURN_INTER_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
    '            End If
    '            qry1 += " ) finalSRIC  " & _
    ' "   union all " & _
    ' "  select * from  " & _
    '" (select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,substring( convert(varchar(11),convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as MD,TSPL_SALE_INVOICE_HEAD.cust_Code,TSPL_SALE_INVOICE_HEAD.cust_Name,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No as DocNo,convert(varchar(11),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    '"( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Rate  " & _
    '"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Rate  " & _
    ' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Rate " & _
    ' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Rate   " & _
    ' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
    '  "          TSPL_SALE_INVOICE_DETAIL.TAX5_Rate " & _
    '  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
    '   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Rate " & _
    ' " else 0  end end end end end end) as TxRate, TSPL_SALE_INVOICE_DETAIL.Total_net_Amt , " & _
    ' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt   " & _
    '  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.Tax2_Assessable_Amt   " & _
    '  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.Tax3_Assessable_Amt  " & _
    '  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.Tax4_Assessable_Amt   " & _
    '  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
    '   "         TSPL_SALE_INVOICE_DETAIL.Tax5_Assessable_Amt " & _
    ' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
    '  "          TSPL_SALE_INVOICE_DETAIL.Tax6_Assessable_Amt " & _
    '  " else 0  end end end end end end) * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxBaseAmt, " & _
    ' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Amt  " & _
    '  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Amt   " & _
    '  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Amt " & _
    '  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Amt   " & _
    '  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
    '   "         TSPL_SALE_INVOICE_DETAIL.TAX5_Amt " & _
    '  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
    '   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Amt " & _
    '   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Tin_No as compTin   ,TSPL_SALE_INVOICE_HEAD.Against_C_Form as cform" & _
    '   "         from TSPL_SALE_INVOICE_DETAIL " & _
    '  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
    '" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
    '" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
    '  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
    '  " where 2=2  AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
    '            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '                qry1 += "and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '            End If
    '            qry1 += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' ) finalSI  " & _
    '           " where TxRate <> 0 )abc where 2=2 and TxRate <> 0  "


    '            If chkselectcustomer.IsChecked Then
    '                qry1 += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
    '            End If
    '            qry1 += ")  main group by   cust_Code,cust_Name,Mdate,MD,Tin_No,TxRate  "


    '            'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)


    '            Dim strmain As String = qry1 + " union all " + qry
    '            Dim frm As New FrmSalerReport()

    '            frm.funreport(strmain, "DetailsOfForm2BCstVat", "Details of Form 2B")




    '            ''Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            ''If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
    '            ''    Dim frm As New FrmSalerReport()

    '            ''    frm.funreport(qry1, "DetailsOfForm2BCST", "Details of Form 2B")
    '            ''Else
    '            ''    Dim frm As New FrmSalerReport()
    '            ''    ' frm.funSubreport(qry1, qry, "DetailsOfForm2B", "Details of Form 2B", "DetailsOfForm2BVAT.rpt")
    '            ''    frm.funSubreport(qry, qry1, "DetailsOfForm2BVAT", "Details of Form 2B", "DetailsOfForm2B.rpt")
    '            ''    'CommonServicesViewer.funreport(dt1, "DetailsOfForm2B", "Details of Form 2B")
    '            ''End If



    '            '---------------------------------------------------------------------------------------------------

    '            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '            '    Throw New Exception("No Data found to Print")
    '            'Else
    '            '    CommonServicesViewer.funreport(dt, "DetailsOfForm2BVAT", "Details of Form 2B Vat")
    '            'End If








    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(ex.Message)
    '        End Try
    '    End Sub



    Sub print()
        Try
            If chkselectcustomer.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One customer")
                Return
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Atleast Single Location Or select All ")
                Return
            End If
            Dim qry As String

            qry = "  select 'vat' as type,max (FDaTe) as FDaTe,max(todate) as todate,Mdate,MD,cust_Code,cust_Name,Tin_No,TxRate,SUM ( TxBaseAmt) as TxBaseAmt,sum (Total_net_Amt) as Total_net_Amt, sum (TxAmt) as TxAmt ,MAX( compTin) as compTin  ,max(cform) as cform from  ( select * from(  " & _
                  " select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)) as MD,TSPL_SALE_RETURN_HEAD.cust_Code,TSPL_SALE_RETURN_HEAD.cust_Name,TSPL_SALE_RETURN_DETAIL.Sale_Return_No as DocNo,convert(varchar(12),TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No ," & _
                  " ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Rate  " & _
                  "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX2_Rate          else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX3_Rate " & _
                  "   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX4_Rate             else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX5_Rate " & _
                  "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then   TSPL_SALE_RETURN_DETAIL.TAX6_Rate " & _
                  "  else 0  end end end end end end) as TxRate, (-1 *  TSPL_SALE_RETURN_DETAIL.Total_net_Amt) as Total_net_Amt , " & _
                  "  -1 *  ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Assessable_Amt    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax2_Assessable_Amt    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax3_Assessable_Amt   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.Tax4_Assessable_Amt    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax5_Assessable_Amt  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_DETAIL.Return_Qty as TxBaseAmt, " & _
                  "  -1 *  ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Amt  " & _
                 "else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX2_Amt " & _
 " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX3_Amt " & _
" else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX4_Amt " & _
" else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX5_Amt " & _
" else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_DETAIL.TAX6_Amt " & _
"  else 0  end end end end end end)* TSPL_SALE_RETURN_DETAIL.Return_Qty as TxAmt, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1 , TSPL_COMPANY_MASTER.Tin_No as compTin   ,'' as cform   from TSPL_SALE_RETURN_DETAIL " & _
   "  left outer join TSPL_SALE_RETURN_HEAD  on TSPL_SALE_RETURN_DETAIL.Sale_Return_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No  " & _
   "  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code " & _
   "  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_HEAD.Cust_Code " & _
   "  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_HEAD.Comp_Code  " & _
    " where 2=2  and TSPL_SALE_RETURN_HEAD.is_post='Y' and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + " ' and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + " ' "
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry += " and  TSPL_SALE_RETURN_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If
            qry += "  union all " & _
                 " select FDaTe,todate,Mdate,MD,cust_code,cust_Name,DocNo,posting_Date,Tin_No,TxRate,Total_net_Amt,TxBaseAmt,TxAmt,Comp_Name,Add1,compTin,cform from (  select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)) as MD,TSPL_Customer_Invoice_Head.Customer_Code as cust_code,TSPL_Customer_Invoice_Head.Customer_Name as cust_Name,(case when TSPL_Customer_Invoice_Head.AgainstScrap='' then  TSPL_Customer_Invoice_Head.Document_No else TSPL_Customer_Invoice_Head.AgainstScrap end ) as DocNo,convert(varchar(11),TSPL_Customer_Invoice_Head.Document_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    "  ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.TAX1_Rate  " & _
     "   else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.TAX2_Rate          else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.TAX3_Rate  " & _
       "   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.TAX4_Rate             else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then TSPL_Customer_Invoice_Head.TAX5_Rate  " & _
        "   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then   TSPL_Customer_Invoice_Head.TAX6_Rate      else 0  end end end end end end) as TxRate,TSPL_Customer_Invoice_Head.Amount_Less_Discount as Total_net_Amt ,  " & _
        "   ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.Tax1_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.Tax2_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.Tax3_BAmount   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.Tax4_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then TSPL_Customer_Invoice_Head.Tax5_BAmount  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then  TSPL_Customer_Invoice_Head.Tax6_BAmount  else 0  end end end end end end)  as TxBaseAmt,  " & _
      " ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.TAX1_Amt   " & _
      "    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.TAX2_Amt  " & _
  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.TAX3_Amt  " & _
  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.TAX4_Amt  " & _
  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then TSPL_Customer_Invoice_Head.TAX5_Amt  " & _
  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then  TSPL_Customer_Invoice_Head.TAX6_Amt  " & _
  "  else 0  end end end end end end) as TxAmt   ,TSPL_COMPANY_MASTER.Tin_No as compTin  ,'' as cform , " & _
  " (select substring(TSPL_Customer_Invoice_Detail.GL_Account_Code,len(TSPL_Customer_Invoice_Detail.GL_Account_Code)-2,4) from TSPL_Customer_Invoice_Detail where TSPL_Customer_Invoice_Detail.Document_No=TSPL_Customer_Invoice_Head.Document_No and SNo='1' )as loc,  " & _
        "      TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1  " & _
         "     from TSPL_Customer_Invoice_Head  " & _
   "   left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code   " & _
    "   left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Customer_Invoice_Head.Comp_Code   " & _
    "   where 2=2 and TSPL_Customer_Invoice_Head.status='1'  and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "') finalAR where 2=2   "
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry += " and  finalAR.loc in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            qry += "  union all " & _
  " select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)) as MD,TSPL_SALE_RETURN_INTER_HEAD.cust_Code,TSPL_SALE_RETURN_INTER_HEAD.cust_Name,TSPL_SALE_RETURN_INTER_DETAIL.Document_No as DocNo,convert(varchar(11),TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No ," & _
 " ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Rate " & _
   " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Rate     else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Rate     else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Rate    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Rate   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then   TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Rate    else 0  end end end end end end) as TxRate,(-1 *  TSPL_SALE_RETURN_INTER_DETAIL.Total_net_Amt) as Total_net_Amt , " & _
 "-1 *  (case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1)='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Assessable_Amt  else case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V' then TSPL_SALE_RETURN_INTER_DETAIL.Tax2_Assessable_Amt  " & _
 " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax3_Assessable_Amt   " & _
"  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.Tax4_Assessable_Amt " & _
"  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax5_Assessable_Amt " & _
 "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_INTER_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_INTER_DETAIL.Qty as TxBaseAmt, " & _
  " -1 * ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt  " & _
  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt " & _
  "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt " & _
    " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt " & _
   " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt " & _
   " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Amt " & _
    " else 0  end end end end end end)* TSPL_SALE_RETURN_INTER_DETAIL.qty as TxAmt, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Tin_No as compTin   ,'' as cform  from TSPL_SALE_RETURN_INTER_DETAIL " & _
    " left outer join TSPL_SALE_RETURN_INTER_HEAD  on TSPL_SALE_RETURN_INTER_DETAIL.Document_No=TSPL_SALE_RETURN_INTER_HEAD.Document_No " & _
    " left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_INTER_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code " & _
    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_INTER_HEAD.Cust_Code " & _
    " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_INTER_HEAD.Comp_Code  " & _
    " where 2=2  and TSPL_SALE_RETURN_INTER_HEAD.is_post='1' and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "'"
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry += " and  TSPL_SALE_RETURN_INTER_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            qry += "  union all " & _
 "  select * from  " & _
" (select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as MD,TSPL_SALE_INVOICE_HEAD.cust_Code,TSPL_SALE_INVOICE_HEAD.cust_Name,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No as DocNo,convert(varchar(11),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
"( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Rate  " & _
"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Rate  " & _
 " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Rate " & _
 " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Rate   " & _
 " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
  "          TSPL_SALE_INVOICE_DETAIL.TAX5_Rate " & _
  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Rate " & _
 " else 0  end end end end end end) as TxRate,TSPL_SALE_INVOICE_DETAIL.Total_net_Amt , " & _
 " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt   " & _
  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.Tax2_Assessable_Amt   " & _
  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.Tax3_Assessable_Amt  " & _
  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.Tax4_Assessable_Amt   " & _
  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
   "         TSPL_SALE_INVOICE_DETAIL.Tax5_Assessable_Amt " & _
 " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
  "          TSPL_SALE_INVOICE_DETAIL.Tax6_Assessable_Amt " & _
  " else 0  end end end end end end) * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxBaseAmt, " & _
 " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Amt  " & _
  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Amt   " & _
  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Amt " & _
  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Amt   " & _
  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
   "         TSPL_SALE_INVOICE_DETAIL.TAX5_Amt " & _
  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Amt " & _
   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Tin_No as compTin   ,TSPL_SALE_INVOICE_HEAD.Against_C_Form as cform " & _
   "         from TSPL_SALE_INVOICE_DETAIL " & _
  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
  " where 2=2 and  TSPL_SALE_INVOICE_HEAD.is_post='Y' "
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry += " and  TSPL_SALE_INVOICE_DETAIL.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If


            qry += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "' ) finalSI  where TxRate <> 0   "



            'If chkselectcustomer.IsChecked Then
            '    qry += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
            'End If
            'qry += ")asdf where TxRate<>0)  main  where 2=2 and TxRate <> 0"

            qry += ")asdf where (TxAmt<>0 or TxRate<>0))  main  where 2=2 and (TxAmt<>0 or TxRate<>0)"

            If chkselectcustomer.IsChecked Then
                qry += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
            End If
            qry += " group by   cust_Code,cust_Name,Mdate,MD,Tin_No,TxRate "






            '----------------------For cst tax------------------------------------------------------------------




            Dim qry1 As String

            qry1 = "  select 'cst' as type,max (FDaTe) as FDaTe,max(todate) as todate,Mdate,MD,cust_Code,cust_Name,Tin_No,TxRate,SUM ( TxBaseAmt) as TxBaseAmt,sum (Total_net_Amt) as Total_net_Amt, sum (TxAmt) as TxAmt ,MAX( compTin) as compTin  ,max(cform) as cform from(  select * from  ( " & _
           " Select FDaTe,todate,Mdate,MD,cust_code,cust_name,DocNo,posting_Date,Tin_No,TxRate,Total_net_Amt,TxBaseAmt,TxAmt,Comp_Name,Add1,compTin,cform from(select '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_Customer_Invoice_Head.posting_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_Customer_Invoice_Head.posting_Date,103)) as MD,TSPL_Customer_Invoice_Head.Customer_Code as cust_code,TSPL_Customer_Invoice_Head.Customer_Name as cust_name,TSPL_Customer_Invoice_Head.Document_No as DocNo,convert(date,TSPL_Customer_Invoice_Head.posting_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No ,   " & _
" ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='c'  then TSPL_Customer_Invoice_Head.TAX1_Rate  " & _
" else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='c'   then TSPL_Customer_Invoice_Head.TAX2_Rate " & _
 " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='c'   then TSPL_Customer_Invoice_Head.TAX3_Rate " & _
 "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='c'  then TSPL_Customer_Invoice_Head.TAX4_Rate " & _
 " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='c'   then   TSPL_Customer_Invoice_Head.TAX5_Rate       else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='c'   then   TSPL_Customer_Invoice_Head.TAX6_Rate  " & _
 "  else 0  end end end end end end) as TxRate, " & _
 "   TSPL_Customer_Invoice_Head.Amount_Less_Discount as Total_net_Amt , " & _
   "  ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='c'  then TSPL_Customer_Invoice_Head.Tax1_BAmount    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='c'   then TSPL_Customer_Invoice_Head.Tax2_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='c'   then TSPL_Customer_Invoice_Head.Tax3_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='c'  then TSPL_Customer_Invoice_Head.Tax4_BAmount     else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='c'   then   TSPL_Customer_Invoice_Head.Tax5_BAmount     else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='c'   then   TSPL_Customer_Invoice_Head.Tax6_BAmount     else 0  end end end end end end) as TxBaseAmt, " & _
   " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='c'  then TSPL_Customer_Invoice_Head.TAX1_Amt " & _
   "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='c'   then TSPL_Customer_Invoice_Head.TAX2_Amt    else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='c'   then TSPL_Customer_Invoice_Head.TAX3_Amt " & _
  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='c'  then TSPL_Customer_Invoice_Head.TAX4_Amt    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='c'   then  TSPL_Customer_Invoice_Head.TAX5_Amt " & _
 "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='c'   then   TSPL_Customer_Invoice_Head.TAX6_Amt " & _
   "  else 0  end end end end end end) as TxAmt," & _
   "  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1   ,TSPL_COMPANY_MASTER.Tin_No as compTin  ,'' as cform ,   (select substring(TSPL_Customer_Invoice_Detail.GL_Account_Code,len(TSPL_Customer_Invoice_Detail.GL_Account_Code)-2,4) from TSPL_Customer_Invoice_Detail where TSPL_Customer_Invoice_Detail.Document_No=TSPL_Customer_Invoice_Head.Document_No and SNo='1' )as loc    from TSPL_Customer_Invoice_Head " & _
" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code  " & _
 " LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Customer_Invoice_Head.Comp_Code   where 2=2 AND TSPL_Customer_Invoice_Head.Status='1'  "
            qry1 += " ) finalARC where   convert(date, posting_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and posting_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' "
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry1 += " and loc in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            qry1 += "   union all " & _
  "   select * from  (  select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103),105),4,7 )as Mdate ,month(convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)) as MD," & _
      " TSPL_SALE_RETURN_HEAD.cust_Code,TSPL_SALE_RETURN_HEAD.cust_Name,TSPL_SALE_RETURN_DETAIL.Sale_Return_No as DocNo,convert(varchar(11),TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
 " ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX1_Rate  " & _
   " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_DETAIL.TAX2_Rate   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_DETAIL.TAX3_Rate " & _
  "   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX4_Rate  " & _
  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='c'   then  TSPL_SALE_RETURN_DETAIL.TAX5_Rate " & _
   " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='c'   then   TSPL_SALE_RETURN_DETAIL.TAX6_Rate " & _
   "  else 0  end end end end end end) as TxRate,-1 *( TSPL_SALE_RETURN_DETAIL.Total_net_Amt) as Total_net_Amt , " & _
   " -1 *( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX1_Assessable_Amt       else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_DETAIL.Tax2_Assessable_Amt    else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_DETAIL.Tax3_Assessable_Amt   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_DETAIL.Tax4_Assessable_Amt    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='c'   then          TSPL_SALE_RETURN_DETAIL.Tax5_Assessable_Amt  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='c'   then           TSPL_SALE_RETURN_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_DETAIL.Return_Qty as TxBaseAmt, " & _
   " -1 * ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX1_Amt  " & _
   "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_DETAIL.TAX2_Amt " & _
   "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_DETAIL.TAX3_Amt " & _
   "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_DETAIL.TAX4_Amt   " & _
   "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='c'   then   TSPL_SALE_RETURN_DETAIL.TAX5_Amt " & _
   "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='c'   then      TSPL_SALE_RETURN_DETAIL.TAX6_Amt  " & _
   "  else 0  end end end end end end)* TSPL_SALE_RETURN_DETAIL.Return_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,          TSPL_COMPANY_MASTER.Tin_No as compTin   ,'' as cform   " & _
   "   from TSPL_SALE_RETURN_DETAIL  left outer join TSPL_SALE_RETURN_HEAD  on TSPL_SALE_RETURN_DETAIL.Sale_Return_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_HEAD.Cust_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_HEAD.Comp_Code  where 2=2  AND TSPL_SALE_RETURN_HEAD.Is_Post='Y' " & _
"   and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "'  "
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry1 += " and  TSPL_SALE_RETURN_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If
            qry1 += " ) finalSRC  " & _
  "   union all " & _
"  select * from   (  select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),105),4,7 )as Mdate ,month(convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)) as MD, " & _
     "  TSPL_SALE_RETURN_INTER_HEAD.cust_Code,TSPL_SALE_RETURN_INTER_HEAD.cust_Name,TSPL_SALE_RETURN_INTER_DETAIL.Document_No as DocNo,convert(varchar(11),TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
 " ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Rate  " & _
  "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Rate   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Rate " & _
  "   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Rate   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='c'   then  TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Rate " & _
  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='c'   then   TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Rate  else 0  end end end end end end) as TxRate, -1 * (TSPL_SALE_RETURN_INTER_DETAIL.Total_net_Amt) as Total_net_Amt , " & _
  " -1 * ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Assessable_Amt       else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax2_Assessable_Amt    else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax3_Assessable_Amt   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.Tax4_Assessable_Amt    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='c'   then          TSPL_SALE_RETURN_INTER_DETAIL.Tax5_Assessable_Amt  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='c'   then           TSPL_SALE_RETURN_INTER_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_INTER_DETAIL.Qty as TxBaseAmt, " & _
 " -1 * ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt  " & _
 " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt  " & _
"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='c'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt " & _
"  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='c'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt " & _
"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='c'   then   TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='c'   then      TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Amt   else 0  end end end end end end)* TSPL_SALE_RETURN_INTER_DETAIL.Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,          TSPL_COMPANY_MASTER.Tin_No as compTin   ,'' as cform  " & _
  "    from TSPL_SALE_RETURN_INTER_DETAIL  left outer join TSPL_SALE_RETURN_INTER_HEAD  on TSPL_SALE_RETURN_INTER_DETAIL.Document_No=TSPL_SALE_RETURN_INTER_HEAD.Document_No  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_INTER_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_INTER_HEAD.Comp_Code  where 2=2  AND TSPL_SALE_RETURN_INTER_HEAD.Is_Post='1' " & _
"   and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "'  "
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry1 += " and  TSPL_SALE_RETURN_INTER_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If
            qry1 += " ) finalSRIC  " & _
 "   union all " & _
 "  select * from  " & _
" (select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,substring( convert(varchar(11),convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as MD,TSPL_SALE_INVOICE_HEAD.cust_Code,TSPL_SALE_INVOICE_HEAD.cust_Name,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No as DocNo,convert(varchar(11),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
"( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Rate  " & _
"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Rate  " & _
 " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Rate " & _
 " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Rate   " & _
 " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
  "          TSPL_SALE_INVOICE_DETAIL.TAX5_Rate " & _
  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Rate " & _
 " else 0  end end end end end end) as TxRate, TSPL_SALE_INVOICE_DETAIL.Total_net_Amt , " & _
 " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt   " & _
  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.Tax2_Assessable_Amt   " & _
  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.Tax3_Assessable_Amt  " & _
  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.Tax4_Assessable_Amt   " & _
  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
   "         TSPL_SALE_INVOICE_DETAIL.Tax5_Assessable_Amt " & _
 " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
  "          TSPL_SALE_INVOICE_DETAIL.Tax6_Assessable_Amt " & _
  " else 0  end end end end end end) * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxBaseAmt, " & _
 " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Amt  " & _
  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Amt   " & _
  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Amt " & _
  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Amt   " & _
  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
   "         TSPL_SALE_INVOICE_DETAIL.TAX5_Amt " & _
  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Amt " & _
   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Tin_No as compTin   ,TSPL_SALE_INVOICE_HEAD.Against_C_Form as cform" & _
   "         from TSPL_SALE_INVOICE_DETAIL " & _
  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
  " where 2=2  AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry1 += "and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            qry1 += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' ) finalSI  " & _
           " where TxRate <> 0 )abc where 2=2 and TxRate <> 0  "


            If chkselectcustomer.IsChecked Then
                qry1 += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
            End If
            qry1 += ")  main group by   cust_Code,cust_Name,Mdate,MD,Tin_No,TxRate  "


            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)


            Dim strmain As String = qry1 + " union all " + qry



            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strmain), "DetailsOfForm2BCstVat", "Details of Form 2B")

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    '' ''    Sub print()
    '' ''        Try
    '' ''            If chkselectcustomer.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
    '' ''                common.clsCommon.MyMessageBoxShow("Please Select Atleast One customer")
    '' ''                Return
    '' ''            End If
    '' ''            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
    '' ''                common.clsCommon.MyMessageBoxShow("Please select Atleast Single Location Or select All ")
    '' ''                Return
    '' ''            End If
    '' ''            Dim qry As String

    '' ''            qry = "  select 'vat' as type,max (FDaTe) as FDaTe,max(todate) as todate,Mdate,MD,cust_Code,cust_Name,Tin_No,TxRate,SUM ( TxBaseAmt) as TxBaseAmt,sum (Total_net_Amt) as Total_net_Amt, sum (TxAmt) as TxAmt ,MAX( compTin) as compTin  ,max(cform) as cform from(  select * from ( select * from ( " & _
    '' ''            "  select '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103)) as MD,TSPL_SCRAPINVOICE_HEAD.cust_Code,TSPL_SCRAPINVOICE_HEAD.cust_Name,TSPL_SCRAPINVOICE_HEAD.invoice_No as DocNo,convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    '' ''         "  ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Rate   " & _
    '' ''         "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='V'   then TSPL_SCRAPINVOICE_HEAD.TAX2_Rate   " & _
    '' ''       "   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='V'   then TSPL_SCRAPINVOICE_HEAD.TAX3_Rate " & _
    '' ''  "  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX4_Rate   " & _
    '' '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='V'   then " & _
    '' ''"  TSPL_SCRAPINVOICE_HEAD.TAX5_Rate " & _
    '' '' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='V'   then " & _
    '' '' "  TSPL_SCRAPINVOICE_HEAD.TAX6_Rate " & _
    '' ''"  else 0  end end end end end end) as TxRate,  TSPL_SCRAPINVOICE_HEAD.Amount_Less_Discount as Total_net_Amt , " & _
    '' ''"   ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Base_Amt   " & _
    '' '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='V'   then TSPL_SCRAPINVOICE_HEAD.Tax2_Base_Amt   " & _
    '' '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='V'   then TSPL_SCRAPINVOICE_HEAD.Tax3_Base_Amt   " & _
    '' '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='V'  then TSPL_SCRAPINVOICE_HEAD.Tax4_Base_Amt   " & _
    '' ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='V'   then " & _
    '' ''"  TSPL_SCRAPINVOICE_HEAD.Tax5_Base_Amt   " & _
    '' ''"  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='V'   then " & _
    '' '' "  TSPL_SCRAPINVOICE_HEAD.Tax6_Base_Amt   " & _
    '' ''"  else 0  end end end end end end) as TxBaseAmt, " & _
    '' '' "  ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Amt   " & _
    '' ''" else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='V'   then TSPL_SCRAPINVOICE_HEAD.TAX2_Amt   " & _
    '' ''" else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='V'   then TSPL_SCRAPINVOICE_HEAD.TAX3_Amt " & _
    '' ''"  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='V'  then TSPL_SCRAPINVOICE_HEAD.TAX4_Amt  " & _
    '' ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='V'   then " & _
    '' '' " TSPL_SCRAPINVOICE_HEAD.TAX5_Amt " & _
    '' ''"  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='V'   then " & _
    '' '' "  TSPL_SCRAPINVOICE_HEAD.TAX6_Amt " & _
    '' ''"  else 0  end end end end end end) as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1   ,TSPL_COMPANY_MASTER.Tin_No as compTin  ,'' as cform " & _
    '' ''"     from TSPL_SCRAPINVOICE_HEAD " & _
    '' ''"    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code " & _
    '' ''"    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code " & _
    '' '' "  LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SCRAPINVOICE_HEAD.Comp_Code " & _
    '' ''"  where 2=2 AND TSPL_SCRAPINVOICE_HEAD.ispost=1 "
    '' ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '' ''                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '' ''            End If
    '' ''            qry += " ) finalSC where   convert(date, posting_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and posting_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' " & _
    '' '' "   union all " & _
    '' '' "  select * from  " & _
    '' ''" (select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,substring( convert(varchar(11),convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as MD,TSPL_SALE_INVOICE_HEAD.cust_Code,TSPL_SALE_INVOICE_HEAD.cust_Name,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No as DocNo,convert(varchar(11),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    '' ''"( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Rate  " & _
    '' ''"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Rate  " & _
    '' '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Rate " & _
    '' '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Rate   " & _
    '' '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    '' ''  "          TSPL_SALE_INVOICE_DETAIL.TAX5_Rate " & _
    '' ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    '' ''   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Rate " & _
    '' '' " else 0  end end end end end end) as TxRate, TSPL_SALE_INVOICE_DETAIL.Total_net_Amt , " & _
    '' '' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt   " & _
    '' ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.Tax2_Assessable_Amt   " & _
    '' ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.Tax3_Assessable_Amt  " & _
    '' ''  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.Tax4_Assessable_Amt   " & _
    '' ''  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    '' ''   "         TSPL_SALE_INVOICE_DETAIL.Tax5_Assessable_Amt " & _
    '' '' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    '' ''  "          TSPL_SALE_INVOICE_DETAIL.Tax6_Assessable_Amt " & _
    '' ''  " else 0  end end end end end end) * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxBaseAmt, " & _
    '' '' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Amt  " & _
    '' ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Amt   " & _
    '' ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Amt " & _
    '' ''  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Amt   " & _
    '' ''  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    '' ''   "         TSPL_SALE_INVOICE_DETAIL.TAX5_Amt " & _
    '' ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    '' ''   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Amt " & _
    '' ''   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Tin_No as compTin   ,TSPL_SALE_INVOICE_HEAD.Against_C_Form as cform" & _
    '' ''   "         from TSPL_SALE_INVOICE_DETAIL " & _
    '' ''  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
    '' ''" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
    '' ''" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
    '' ''  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
    '' ''  " where 2=2  AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
    '' ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '' ''                qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '' ''            End If
    '' ''            qry += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' ) finalSI  " & _
    '' ''           " where TxRate <> 0 )abc where 2=2 and TxRate <> 0  "


    '' ''            If chkselectcustomer.IsChecked Then
    '' ''                qry += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
    '' ''            End If
    '' ''            qry += ")  main group by   cust_Code,cust_Name,Mdate,MD,Tin_No,TxRate "






    '' ''            '----------------------For cst tax------------------------------------------------------------------




    '' ''            Dim qry1 As String

    '' ''            qry1 = "  select 'cst' as type,max (FDaTe) as FDaTe,max(todate) as todate,Mdate,MD,cust_Code,cust_Name,Tin_No,TxRate,SUM ( TxBaseAmt) as TxBaseAmt,sum (Total_net_Amt) as Total_net_Amt, sum (TxAmt) as TxAmt ,MAX( compTin) as compTin  ,max(cform) as cform from(  select * from ( select * from ( " & _
    '' ''            "  select '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate, substring( convert(varchar(11),convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103)) as MD,TSPL_SCRAPINVOICE_HEAD.cust_Code,TSPL_SCRAPINVOICE_HEAD.cust_Name,TSPL_SCRAPINVOICE_HEAD.invoice_No as DocNo,convert(date,TSPL_SCRAPINVOICE_HEAD.posting_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    '' ''         "  ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Rate   " & _
    '' ''         "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='c'   then TSPL_SCRAPINVOICE_HEAD.TAX2_Rate   " & _
    '' ''       "   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='c'   then TSPL_SCRAPINVOICE_HEAD.TAX3_Rate " & _
    '' ''  "  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX4_Rate   " & _
    '' '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='c'   then " & _
    '' ''"  TSPL_SCRAPINVOICE_HEAD.TAX5_Rate " & _
    '' '' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='c'   then " & _
    '' '' "  TSPL_SCRAPINVOICE_HEAD.TAX6_Rate " & _
    '' ''"  else 0  end end end end end end) as TxRate,  TSPL_SCRAPINVOICE_HEAD.Amount_Less_Discount as Total_net_Amt , " & _
    '' ''"   ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Base_Amt   " & _
    '' '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='c'   then TSPL_SCRAPINVOICE_HEAD.Tax2_Base_Amt   " & _
    '' '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='c'   then TSPL_SCRAPINVOICE_HEAD.Tax3_Base_Amt   " & _
    '' '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='c'  then TSPL_SCRAPINVOICE_HEAD.Tax4_Base_Amt   " & _
    '' ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='c'   then " & _
    '' ''"  TSPL_SCRAPINVOICE_HEAD.Tax5_Base_Amt   " & _
    '' ''"  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='c'   then " & _
    '' '' "  TSPL_SCRAPINVOICE_HEAD.Tax6_Base_Amt   " & _
    '' ''"  else 0  end end end end end end) as TxBaseAmt, " & _
    '' '' "  ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX1  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX1_Amt   " & _
    '' ''" else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX2  )='c'   then TSPL_SCRAPINVOICE_HEAD.TAX2_Amt   " & _
    '' ''" else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX3  )='c'   then TSPL_SCRAPINVOICE_HEAD.TAX3_Amt " & _
    '' ''"  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX4  )='c'  then TSPL_SCRAPINVOICE_HEAD.TAX4_Amt  " & _
    '' ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX5  )='c'   then " & _
    '' '' " TSPL_SCRAPINVOICE_HEAD.TAX5_Amt " & _
    '' ''"  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SCRAPINVOICE_HEAD.TAX6  )='c'   then " & _
    '' '' "  TSPL_SCRAPINVOICE_HEAD.TAX6_Amt " & _
    '' ''"  else 0  end end end end end end) as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1   ,TSPL_COMPANY_MASTER.Tin_No as compTin  ,'' as cform " & _
    '' ''"     from TSPL_SCRAPINVOICE_HEAD " & _
    '' ''"    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code " & _
    '' ''"    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code " & _
    '' '' "  LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SCRAPINVOICE_HEAD.Comp_Code " & _
    '' ''"  where 2=2 AND TSPL_SCRAPINVOICE_HEAD.ispost = 1 "
    '' ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '' ''                qry1 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '' ''            End If
    '' ''            qry1 += " ) finalSC where    posting_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and posting_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' " & _
    '' '' "   union all " & _
    '' '' "  select * from  " & _
    '' ''" (select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,substring( convert(varchar(11),convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),105),4,7 )as Mdate,month(convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as MD,TSPL_SALE_INVOICE_HEAD.cust_Code,TSPL_SALE_INVOICE_HEAD.cust_Name,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No as DocNo,convert(varchar(11),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    '' ''"( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Rate  " & _
    '' ''"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Rate  " & _
    '' '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Rate " & _
    '' '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Rate   " & _
    '' '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
    '' ''  "          TSPL_SALE_INVOICE_DETAIL.TAX5_Rate " & _
    '' ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
    '' ''   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Rate " & _
    '' '' " else 0  end end end end end end) as TxRate, TSPL_SALE_INVOICE_DETAIL.Total_net_Amt , " & _
    '' '' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Assessable_Amt   " & _
    '' ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.Tax2_Assessable_Amt   " & _
    '' ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.Tax3_Assessable_Amt  " & _
    '' ''  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.Tax4_Assessable_Amt   " & _
    '' ''  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
    '' ''   "         TSPL_SALE_INVOICE_DETAIL.Tax5_Assessable_Amt " & _
    '' '' " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
    '' ''  "          TSPL_SALE_INVOICE_DETAIL.Tax6_Assessable_Amt " & _
    '' ''  " else 0  end end end end end end) * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxBaseAmt, " & _
    '' '' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Amt  " & _
    '' ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Amt   " & _
    '' ''  " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='c'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Amt " & _
    '' ''  " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='c'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Amt   " & _
    '' ''  " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='c'   then " & _
    '' ''   "         TSPL_SALE_INVOICE_DETAIL.TAX5_Amt " & _
    '' ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='c'   then " & _
    '' ''   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Amt " & _
    '' ''   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Tin_No as compTin   ,TSPL_SALE_INVOICE_HEAD.Against_C_Form as cform" & _
    '' ''   "         from TSPL_SALE_INVOICE_DETAIL " & _
    '' ''  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
    '' ''" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
    '' ''" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
    '' ''  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
    '' ''  " where 2=2  AND TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
    '' ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    '' ''                qry1 += "and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '' ''            End If
    '' ''            qry1 += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy/MM/dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy/MM/dd") + "' ) finalSI  " & _
    '' ''           " where TxRate <> 0 )abc where 2=2 and TxRate <> 0  "


    '' ''            If chkselectcustomer.IsChecked Then
    '' ''                qry1 += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
    '' ''            End If
    '' ''            qry1 += ")  main group by   cust_Code,cust_Name,Mdate,MD,Tin_No,TxRate  "


    '' ''            'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)


    '' ''            Dim strmain As String = qry1 + " union all " + qry
    '' ''            Dim frm As New FrmSalerReport()

    '' ''            frm.funreport(strmain, "DetailsOfForm2BCstVat", "Details of Form 2B")




    '' ''            ''Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
    '' ''            ''If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
    '' ''            ''    Dim frm As New FrmSalerReport()

    '' ''            ''    frm.funreport(qry1, "DetailsOfForm2BCST", "Details of Form 2B")
    '' ''            ''Else
    '' ''            ''    Dim frm As New FrmSalerReport()
    '' ''            ''    ' frm.funSubreport(qry1, qry, "DetailsOfForm2B", "Details of Form 2B", "DetailsOfForm2BVAT.rpt")
    '' ''            ''    frm.funSubreport(qry, qry1, "DetailsOfForm2BVAT", "Details of Form 2B", "DetailsOfForm2B.rpt")
    '' ''            ''    'CommonServicesViewer.funreport(dt1, "DetailsOfForm2B", "Details of Form 2B")
    '' ''            ''End If



    '' ''            '---------------------------------------------------------------------------------------------------

    '' ''            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '' ''            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '' ''            '    Throw New Exception("No Data found to Print")
    '' ''            'Else
    '' ''            '    CommonServicesViewer.funreport(dt, "DetailsOfForm2BVAT", "Details of Form 2B Vat")
    '' ''            'End If








    '' ''        Catch ex As Exception
    '' ''            common.clsCommon.MyMessageBoxShow(ex.Message)
    '' ''        End Try
    '' ''    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub


End Class
