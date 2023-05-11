'' Anubhooti(1-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common

Public Class FrmDVAT31

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim refreshGrid As String = "F"


    Public Sub reset()
        Try
            fromdate.Value = clsCommon.GETSERVERDATE()
            Todate.Value = clsCommon.GETSERVERDATE()
            LoadCustomer()
            LoadLocation()
            chkallcustomer.IsChecked = True
            chkLocAll.IsChecked = True
            gv.DataSource = Nothing
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
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.DVAT31)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(1-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "DVAT-31"
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

    Private Sub chkallcustomer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkallcustomer.ToggleStateChanged
        cbgCustomer.Enabled = Not chkallcustomer.IsChecked
    End Sub


    Function print() As String
        Try
            If chkselectcustomer.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One customer")
                Return Nothing
                Exit Function
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Atleast Single Location Or select All ")
                Return Nothing
                Exit Function
            End If
            Dim qry As String
            Dim str As String = "DVAT31 Report"
            'Dim head1 As String
            'Dim head2 As String

            qry = "  select * from (  " & _
                  " select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)) as MD,TSPL_SALE_RETURN_HEAD.cust_Code,TSPL_SALE_RETURN_HEAD.cust_Name,TSPL_SALE_RETURN_DETAIL.Sale_Return_No as DocNo,convert(varchar(12),TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No ," & _
                  " ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Rate  " & _
                  "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX2_Rate          else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX3_Rate " & _
                  "   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX4_Rate             else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX5_Rate " & _
                  "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then   TSPL_SALE_RETURN_DETAIL.TAX6_Rate " & _
                  "  else 0  end end end end end end) as TxRate, " & _
                  "  -1 * ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Assessable_Amt    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax2_Assessable_Amt    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax3_Assessable_Amt   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.Tax4_Assessable_Amt    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax5_Assessable_Amt  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_DETAIL.Return_Qty as TxBaseAmt, " & _
                  "   -1 * ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Amt  " & _
                 "else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX2_Amt " & _
 " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX3_Amt " & _
" else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX4_Amt " & _
" else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX5_Amt " & _
" else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_DETAIL.TAX6_Amt " & _
"  else 0  end end end end end end)* TSPL_SALE_RETURN_DETAIL.Return_Qty as TxAmt,-1 * TSPL_SALE_RETURN_DETAIL.Total_net_Amt  as saleamt, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1   from TSPL_SALE_RETURN_DETAIL " & _
   "  left outer join TSPL_SALE_RETURN_HEAD  on TSPL_SALE_RETURN_DETAIL.Sale_Return_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No  " & _
   "  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code " & _
   "  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_HEAD.Cust_Code " & _
   "  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_HEAD.Comp_Code  " & _
    " where 2=2  and TSPL_SALE_RETURN_HEAD.is_post='Y' and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + " ' and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + " ' "
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry += " and  TSPL_SALE_RETURN_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If
            qry += "  union all " & _
                 " select FDaTe,todate,Mdate,MD,cust_code,cust_Name,DocNo,posting_Date,Tin_No,TxRate,TxBaseAmt,TxAmt,saleamt,Comp_Name,Add1 from (  select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)) as MD,TSPL_Customer_Invoice_Head.Customer_Code as cust_code,TSPL_Customer_Invoice_Head.Customer_Name as cust_Name,(case when TSPL_Customer_Invoice_Head.AgainstScrap='' then  TSPL_Customer_Invoice_Head.Document_No else TSPL_Customer_Invoice_Head.AgainstScrap end ) as DocNo,convert(varchar(11),TSPL_Customer_Invoice_Head.Document_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    "  ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.TAX1_Rate  " & _
     "   else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.TAX2_Rate          else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.TAX3_Rate  " & _
       "   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.TAX4_Rate             else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then TSPL_Customer_Invoice_Head.TAX5_Rate  " & _
        "   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then   TSPL_Customer_Invoice_Head.TAX6_Rate      else 0  end end end end end end) as TxRate,  " & _
        "   ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.Tax1_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.Tax2_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.Tax3_BAmount   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.Tax4_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then TSPL_Customer_Invoice_Head.Tax5_BAmount  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then  TSPL_Customer_Invoice_Head.Tax6_BAmount  else 0  end end end end end end)  as TxBaseAmt,  " & _
      " ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.TAX1_Amt   " & _
      "    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.TAX2_Amt  " & _
  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.TAX3_Amt  " & _
  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.TAX4_Amt  " & _
  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then TSPL_Customer_Invoice_Head.TAX5_Amt  " & _
  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then  TSPL_Customer_Invoice_Head.TAX6_Amt  " & _
  "  else 0  end end end end end end) as TxAmt,  TSPL_Customer_Invoice_Head.Amount_Less_Discount as saleamt,  " & _
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
   " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Rate     else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Rate     else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Rate    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Rate   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then   TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Rate    else 0  end end end end end end) as TxRate, " & _
 "-1 * (case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1)='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Assessable_Amt  else case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V' then TSPL_SALE_RETURN_INTER_DETAIL.Tax2_Assessable_Amt  " & _
 " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax3_Assessable_Amt   " & _
"  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.Tax4_Assessable_Amt " & _
"  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax5_Assessable_Amt " & _
 "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_INTER_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_INTER_DETAIL.Qty as TxBaseAmt, " & _
  " -1 *( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt  " & _
  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt " & _
  "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt " & _
    " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt " & _
   " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt " & _
   " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Amt " & _
    " else 0  end end end end end end)* TSPL_SALE_RETURN_INTER_DETAIL.qty as TxAmt, -1 * TSPL_SALE_RETURN_INTER_DETAIL.Total_net_Amt as saleamt , TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1  from TSPL_SALE_RETURN_INTER_DETAIL " & _
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
 " else 0  end end end end end end) as TxRate, " & _
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
   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_SALE_INVOICE_DETAIL.Total_net_Amt as saleamt ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 " & _
   "         from TSPL_SALE_INVOICE_DETAIL " & _
  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
  " where 2=2 and  TSPL_SALE_INVOICE_HEAD.is_post='Y' "
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry += " and  TSPL_SALE_INVOICE_DETAIL.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If
            ' qry += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "' ) finalSI  " & _
            '" where TxRate <> 0 )abc where 2=2 and TxRate <> 0  "


            qry += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "' ) finalSI  " & _
           " where TxRate <> 0 )abc where 2=2 and TxAmt <> 0  "



            If chkselectcustomer.IsChecked Then
                qry += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
            End If

            ''Dim final As String = " select Mdate as Month ,DocNo  as 'Doc No',posting_Date as 'Doc Date',Tin_No as 'Buyers Tin',Cust_Name as 'Buyer Name','' as 'Inter state Branch/co asignment transfer','' as 'Export Out of India','' as 'Sale Price(excluding cst)','' as 'Central S.Tax','' as  'Total(7+8)', SUM(SalesPrice) as 'Sales Price(Excluding Tax)',SUM(TxAmt20) as 'AMT Taxable 20%',SUM(TxBaseAmt20) as 'DVAT 20%',SUM(TxAmt12) as 'AMT Taxable 12.50%',SUM(TxBaseAmt12) as 'DVAT 12.50%' , SUM(TxAmt5) as 'AMT Taxable 5%',SUM(TxBaseAmt5) as 'DVAT 5%',SUM(OutputTax) as 'Output Tax',SUM(total) as Total from("
            ''final += "select Mdate ,MD ,Cust_Code ,Cust_Name ,DocNo ,posting_Date ,Tin_No ,TxRate ,(TxAmt12 +TxAmt20 +TxAmt5 ) as SalesPrice,TxAmt20 ,TxBaseAmt20 ,TxAmt12 ,TxBaseAmt12 ,TxAmt5 ,TxBaseAmt5 ,(TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5 ) as OutputTax,(TxAmt12 +TxAmt20 +TxAmt5+TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5) as Total "
            ''final += "  from ( select FDaTe ,todate ,Mdate ,MD ,Cust_Code ,Cust_Name ,DocNo ,posting_Date ,Tin_No, TxRate ,  sum(case when TxRate ='20' then TxBaseAmt  else 0 end) as TxAmt20, sum(case when TxRate='20' then TxAmt else 0 end) as TxBaseAmt20, sum(case when TxRate ='12.50' then TxBaseAmt  else 0 end) as TxAmt12, sum(case when TxRate='12.50' then TxAmt else 0 end) as TxBaseAmt12, sum(case when TxRate ='5' then TxBaseAmt  else 0 end) as TxAmt5, sum(case when TxRate='5' then TxAmt else 0 end) as TxBaseAmt5  "
            ''final += " from (" + qry + ") zzzz group by FDaTe  ,todate ,Mdate ,MD ,Cust_Code ,cust_Name ,DocNo ,posting_Date ,Tin_No ,TxRate "
            ''final += "  )qqqq  )LMNO group by Mdate ,MD ,Cust_Code ,cust_Name ,DocNo ,posting_Date ,Tin_No   order by MD,DocNo  "

            Dim final As String = " select Mdate as Month ,DocNo  as 'Doc No',posting_Date as 'Doc Date',Tin_No as 'Buyers Tin',Cust_Name as 'Buyer Name','' as 'Inter state Branch/co asignment transfer','' as 'Export Out of India','' as 'Sale Price(excluding cst)','' as 'Central S.Tax','' as  'Total(7+8)', SUM(saleamt) as 'Sales Price(Excluding Tax)',SUM(TxAmt20) as 'AMT Taxable 20%',SUM(TxBaseAmt20) as 'DVAT 20%',SUM(TxAmt12) as 'AMT Taxable 12.50%',SUM(TxBaseAmt12) as 'DVAT 12.50%' , SUM(TxAmt5) as 'AMT Taxable 5%',SUM(TxBaseAmt5) as 'DVAT 5%',SUM(OutputTax) as 'Output Tax',SUM(total) as Total from("
            final += "select Mdate ,MD ,Cust_Code ,Cust_Name ,DocNo ,posting_Date ,Tin_No ,TxRate ,(TxAmt12 +TxAmt20 +TxAmt5 ) as SalesPrice,TxAmt20 ,TxBaseAmt20 ,TxAmt12 ,TxBaseAmt12 ,TxAmt5 ,TxBaseAmt5 ,(TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5 +TxBaseAmtMann) as OutputTax,(TxAmt12 +TxAmt20 +TxAmt5+TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5 +TxBaseAmtMann+TxAmtMann) as Total ,saleamt"
            final += "  from ( select FDaTe ,todate ,Mdate ,MD ,Cust_Code ,Cust_Name ,DocNo ,posting_Date ,Tin_No, TxRate ,  sum(case when TxRate ='20' then TxBaseAmt  else 0 end) as TxAmt20, sum(case when TxRate='20' then TxAmt else 0 end) as TxBaseAmt20, sum(case when TxRate ='12.50' then TxBaseAmt  else 0 end) as TxAmt12, sum(case when TxRate='12.50' then TxAmt else 0 end) as TxBaseAmt12, sum(case when TxRate ='5' then TxBaseAmt  else 0 end) as TxAmt5, sum(case when TxRate='5' then TxAmt else 0 end) as TxBaseAmt5, sum(case when TxRate ='0' then TxBaseAmt  else 0 end) as TxAmtMann ,sum(case when TxRate='0' then TxAmt else 0 end) as TxBaseAmtMann ,sum(saleamt) as saleamt  "
            final += " from (" + qry + ") zzzz group by FDaTe  ,todate ,Mdate ,MD ,Cust_Code ,cust_Name ,DocNo ,posting_Date ,Tin_No ,TxRate "
            final += "  )qqqq  )LMNO group by Mdate ,MD ,Cust_Code ,cust_Name ,DocNo ,posting_Date ,Tin_No   order by MD,DocNo  "



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
            Return qry
            'head2 = "Summary of sales/Outward Branch Transfer Register"
            'head1 = "Sales For The Tax Period:  "
            'If refreshGrid = "F" Then

            '    If chkexcel.Checked = True Then
            '        Dim arr As New List(Of String)()
            '        arr.Add("Annexure 2B")
            '        arr.Add("" + head2 + "(Month-Wise)")
            '        arr.Add("(See Instruction 6)")
            '        arr.Add("(To be filled along with return)")
            '        arr.Add("Name Of Dealer:  " + objCommonVar.CurrentCompanyName)
            '        arr.Add("" + head1 + "  From:  " + fromdate.Value + "  To: " + Todate.Value + "")
            '        arr.Add("Summary of Sales (As per DVAT-31)")
            '        Dim LocFilter As String
            '        Dim CustFilter As String
            '        If cbgCustomer.CheckedValue.Count > 0 Then
            '            CustFilter = clsCommon.GetMulcallString(cbgCustomer.CheckedValue)
            '            CustFilter = CustFilter.Replace("'", "")
            '            arr.Add("Customer Filter :" + CustFilter + "")
            '        End If
            '        If cbgLocation.CheckedValue.Count > 0 Then
            '            LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            '            LocFilter = LocFilter.Replace("'", "")
            '            arr.Add("Loc Filter :" + LocFilter + "")
            '        End If
            '        clsCommon.MyExportToExcel(str, gv, arr, "Summary of DVAT31")

            '    End If
            'End If
            'If refreshGrid = "F" AndAlso chkexcel.Checked = False Then
            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '        Throw New Exception("No Data found to Print")
            '    Else
            '        CommonServicesViewer.funreport(dt, "RptDVAT31", "DVAT-31")
            '    End If

            'End If
            'refreshGrid = "F"
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return Nothing
    End Function

    Sub printdataExport(ByVal exporter As EnumExportTo)
        Try

            Dim qry As String = print()
            Dim head2 As String = "Summary of sales/Outward Branch Transfer Register"
            Dim head1 As String = "Sales For The Tax Period:  "
            If refreshGrid = "F" Then

                Dim arr As New List(Of String)()
                arr.Add("Annexure 2B")
                arr.Add("" + head2 + "(Month-Wise)")
                arr.Add("(See Instruction 6)")
                arr.Add("(To be filled along with return)")
                arr.Add("Name Of Dealer:  " + objCommonVar.CurrentCompanyName)
                arr.Add("" + head1 + "  From:  " + fromdate.Value + "  To: " + Todate.Value + "")
                arr.Add("Summary of Sales (As per DVAT-31)")
                Dim LocFilter As String
                Dim CustFilter As String
                If cbgCustomer.CheckedValue.Count > 0 Then
                    CustFilter = clsCommon.GetMulcallString(cbgCustomer.CheckedValue)
                    CustFilter = CustFilter.Replace("'", "")
                    arr.Add("Customer Filter :" + CustFilter + "")
                End If
                If cbgLocation.CheckedValue.Count > 0 Then
                    LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                    LocFilter = LocFilter.Replace("'", "")
                    arr.Add("Loc Filter :" + LocFilter + "")
                End If
                'clsCommon.MyExportToExcel("DVAT31 Report", gv, arr, "Summary of DVAT31")

                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcel("DVAT31 Report", gv, arr, Me.Text)
                ElseIf exporter = EnumExportTo.PDF Then
                    clsCommon.MyExportToPDF("DVAT31 Report", gv, arr, Me.Text, True)
                End If
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub printcrystal()
        Try
            Dim qry As String = print()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim fRMcrys As New frmCrystalReportViewer
                fRMcrys.funreport(CrystalReportFolder.CommonServices, dt, "RptDVAT31", "DVAT-31")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
    ''            Dim str As String = "DVAT31 Report"
    ''            Dim head1 As String
    ''            Dim head2 As String

    ''            qry = "  select * from (  " & _
    ''                  " select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)) as MD,TSPL_SALE_RETURN_HEAD.cust_Code,TSPL_SALE_RETURN_HEAD.cust_Name,TSPL_SALE_RETURN_DETAIL.Sale_Return_No as DocNo,convert(varchar(12),TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No ," & _
    ''                  " ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Rate  " & _
    ''                  "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX2_Rate          else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX3_Rate " & _
    ''                  "   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX4_Rate             else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX5_Rate " & _
    ''                  "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then   TSPL_SALE_RETURN_DETAIL.TAX6_Rate " & _
    ''                  "  else 0  end end end end end end) as TxRate, " & _
    ''                  "  -1 * ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Assessable_Amt    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax2_Assessable_Amt    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax3_Assessable_Amt   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.Tax4_Assessable_Amt    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_DETAIL.Tax5_Assessable_Amt  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_DETAIL.Return_Qty as TxBaseAmt, " & _
    ''                  "   -1 * ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX1_Amt  " & _
    ''                 "else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX2_Amt " & _
    '' " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX3_Amt " & _
    ''" else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_DETAIL.TAX4_Amt " & _
    ''" else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_DETAIL.TAX5_Amt " & _
    ''" else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_DETAIL.TAX6_Amt " & _
    ''"  else 0  end end end end end end)* TSPL_SALE_RETURN_DETAIL.Return_Qty as TxAmt,-1 * TSPL_SALE_RETURN_DETAIL.Total_net_Amt  as saleamt, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1   from TSPL_SALE_RETURN_DETAIL " & _
    ''   "  left outer join TSPL_SALE_RETURN_HEAD  on TSPL_SALE_RETURN_DETAIL.Sale_Return_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No  " & _
    ''   "  left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code " & _
    ''   "  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_HEAD.Cust_Code " & _
    ''   "  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_HEAD.Comp_Code  " & _
    ''    " where 2=2  and TSPL_SALE_RETURN_HEAD.is_post='Y' and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + " ' and convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + " ' "
    ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    ''                qry += " and  TSPL_SALE_RETURN_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
    ''            End If
    ''            qry += "  union all " & _
    ''                 " select FDaTe,todate,Mdate,MD,cust_code,cust_Name,DocNo,posting_Date,Tin_No,TxRate,TxBaseAmt,TxAmt,saleamt,Comp_Name,Add1 from (  select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)) as MD,TSPL_Customer_Invoice_Head.Customer_Code as cust_code,TSPL_Customer_Invoice_Head.Customer_Name as cust_Name,(case when TSPL_Customer_Invoice_Head.AgainstScrap='' then  TSPL_Customer_Invoice_Head.Document_No else TSPL_Customer_Invoice_Head.AgainstScrap end ) as DocNo,convert(varchar(11),TSPL_Customer_Invoice_Head.Document_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ''    "  ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.TAX1_Rate  " & _
    ''     "   else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.TAX2_Rate          else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.TAX3_Rate  " & _
    ''       "   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.TAX4_Rate             else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then TSPL_Customer_Invoice_Head.TAX5_Rate  " & _
    ''        "   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then   TSPL_Customer_Invoice_Head.TAX6_Rate      else 0  end end end end end end) as TxRate,  " & _
    ''        "   ( case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.Tax1_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.Tax2_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.Tax3_BAmount   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.Tax4_BAmount    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then TSPL_Customer_Invoice_Head.Tax5_BAmount  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then  TSPL_Customer_Invoice_Head.Tax6_BAmount  else 0  end end end end end end)  as TxBaseAmt,  " & _
    ''      " ( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX1  )='V'  then TSPL_Customer_Invoice_Head.TAX1_Amt   " & _
    ''      "    else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX2  )='V'   then TSPL_Customer_Invoice_Head.TAX2_Amt  " & _
    ''  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX3  )='V'   then TSPL_Customer_Invoice_Head.TAX3_Amt  " & _
    ''  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX4  )='V'  then TSPL_Customer_Invoice_Head.TAX4_Amt  " & _
    ''  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX5  )='V'   then TSPL_Customer_Invoice_Head.TAX5_Amt  " & _
    ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_Customer_Invoice_Head.TAX6  )='V'   then  TSPL_Customer_Invoice_Head.TAX6_Amt  " & _
    ''  "  else 0  end end end end end end) as TxAmt,  TSPL_Customer_Invoice_Head.Amount_Less_Discount as saleamt,  " & _
    ''  " (select substring(TSPL_Customer_Invoice_Detail.GL_Account_Code,len(TSPL_Customer_Invoice_Detail.GL_Account_Code)-2,4) from TSPL_Customer_Invoice_Detail where TSPL_Customer_Invoice_Detail.Document_No=TSPL_Customer_Invoice_Head.Document_No and SNo='1' )as loc,  " & _
    ''        "      TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1  " & _
    ''         "     from TSPL_Customer_Invoice_Head  " & _
    ''   "   left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code   " & _
    ''    "   left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Customer_Invoice_Head.Comp_Code   " & _
    ''    "   where 2=2 and TSPL_Customer_Invoice_Head.status='1'  and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "') finalAR where 2=2   "
    ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    ''                qry += " and  finalAR.loc in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    ''            End If
    ''            qry += "  union all " & _
    ''  " select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)) as MD,TSPL_SALE_RETURN_INTER_HEAD.cust_Code,TSPL_SALE_RETURN_INTER_HEAD.cust_Name,TSPL_SALE_RETURN_INTER_DETAIL.Document_No as DocNo,convert(varchar(11),TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No ," & _
    '' " ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Rate " & _
    ''   " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Rate     else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Rate     else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Rate    else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Rate   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then   TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Rate    else 0  end end end end end end) as TxRate, " & _
    '' "-1 * (case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1)='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Assessable_Amt  else case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V' then TSPL_SALE_RETURN_INTER_DETAIL.Tax2_Assessable_Amt  " & _
    '' " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax3_Assessable_Amt   " & _
    ''"  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.Tax4_Assessable_Amt " & _
    ''"  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.Tax5_Assessable_Amt " & _
    '' "  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_INTER_DETAIL.Tax6_Assessable_Amt  else 0  end end end end end end) * TSPL_SALE_RETURN_INTER_DETAIL.Qty as TxBaseAmt, " & _
    ''  " -1 *( case when(select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt  " & _
    ''  " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt " & _
    ''  "  else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt " & _
    ''    " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4  )='V'  then TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt " & _
    ''   " else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5  )='V'   then TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt " & _
    ''   " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6  )='V'   then  TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Amt " & _
    ''    " else 0  end end end end end end)* TSPL_SALE_RETURN_INTER_DETAIL.qty as TxAmt, -1 * TSPL_SALE_RETURN_INTER_DETAIL.Total_net_Amt as saleamt , TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1  from TSPL_SALE_RETURN_INTER_DETAIL " & _
    ''    " left outer join TSPL_SALE_RETURN_INTER_HEAD  on TSPL_SALE_RETURN_INTER_DETAIL.Document_No=TSPL_SALE_RETURN_INTER_HEAD.Document_No " & _
    ''    " left outer join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_INTER_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code " & _
    ''    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_INTER_HEAD.Cust_Code " & _
    ''    " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_RETURN_INTER_HEAD.Comp_Code  " & _
    ''    " where 2=2  and TSPL_SALE_RETURN_INTER_HEAD.is_post='1' and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "'"
    ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    ''                qry += " and  TSPL_SALE_RETURN_INTER_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
    ''            End If

    ''            qry += "  union all " & _
    '' "  select * from  " & _
    ''" (select  '" + clsCommon.GetPrintDate(fromdate.Value, "dd/MM/yyyy") + "' as FDaTe,'" + clsCommon.GetPrintDate(Todate.Value, "dd/MM/yyyy") + "' as todate,SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),106),' ','-' ),4,10)as Mdate,month(convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as MD,TSPL_SALE_INVOICE_HEAD.cust_Code,TSPL_SALE_INVOICE_HEAD.cust_Name,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No as DocNo,convert(varchar(11),TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as  posting_Date,TSPL_CUSTOMER_MASTER.Tin_No , " & _
    ''"( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX1  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX1_Rate  " & _
    ''"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX2  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX2_Rate  " & _
    '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX3  )='V'   then TSPL_SALE_INVOICE_DETAIL.TAX3_Rate " & _
    '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX4  )='V'  then TSPL_SALE_INVOICE_DETAIL.TAX4_Rate   " & _
    '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX5  )='V'   then " & _
    ''  "          TSPL_SALE_INVOICE_DETAIL.TAX5_Rate " & _
    ''  " else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SALE_INVOICE_DETAIL.TAX6  )='V'   then " & _
    ''   "         TSPL_SALE_INVOICE_DETAIL.TAX6_Rate " & _
    '' " else 0  end end end end end end) as TxRate, " & _
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
    ''   " else 0  end end end end end end)* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as TxAmt,TSPL_SALE_INVOICE_DETAIL.Total_net_Amt as saleamt ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 " & _
    ''   "         from TSPL_SALE_INVOICE_DETAIL " & _
    ''  " left outer join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
    ''" left outer join TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location   = TSPL_LOCATION_MASTER .Location_Code" & _
    ''" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
    ''  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
    ''  " where 2=2 and  TSPL_SALE_INVOICE_HEAD.is_post='Y' "
    ''            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
    ''                qry += " and  TSPL_SALE_INVOICE_DETAIL.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
    ''            End If
    ''            ' qry += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "' ) finalSI  " & _
    ''            '" where TxRate <> 0 )abc where 2=2 and TxRate <> 0  "


    ''            qry += "and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "' ) finalSI  " & _
    ''           " where TxRate <> 0 )abc where 2=2 and TxAmt <> 0  "



    ''            If chkselectcustomer.IsChecked Then
    ''                qry += " and Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
    ''            End If

    ''            ''Dim final As String = " select Mdate as Month ,DocNo  as 'Doc No',posting_Date as 'Doc Date',Tin_No as 'Buyers Tin',Cust_Name as 'Buyer Name','' as 'Inter state Branch/co asignment transfer','' as 'Export Out of India','' as 'Sale Price(excluding cst)','' as 'Central S.Tax','' as  'Total(7+8)', SUM(SalesPrice) as 'Sales Price(Excluding Tax)',SUM(TxAmt20) as 'AMT Taxable 20%',SUM(TxBaseAmt20) as 'DVAT 20%',SUM(TxAmt12) as 'AMT Taxable 12.50%',SUM(TxBaseAmt12) as 'DVAT 12.50%' , SUM(TxAmt5) as 'AMT Taxable 5%',SUM(TxBaseAmt5) as 'DVAT 5%',SUM(OutputTax) as 'Output Tax',SUM(total) as Total from("
    ''            ''final += "select Mdate ,MD ,Cust_Code ,Cust_Name ,DocNo ,posting_Date ,Tin_No ,TxRate ,(TxAmt12 +TxAmt20 +TxAmt5 ) as SalesPrice,TxAmt20 ,TxBaseAmt20 ,TxAmt12 ,TxBaseAmt12 ,TxAmt5 ,TxBaseAmt5 ,(TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5 ) as OutputTax,(TxAmt12 +TxAmt20 +TxAmt5+TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5) as Total "
    ''            ''final += "  from ( select FDaTe ,todate ,Mdate ,MD ,Cust_Code ,Cust_Name ,DocNo ,posting_Date ,Tin_No, TxRate ,  sum(case when TxRate ='20' then TxBaseAmt  else 0 end) as TxAmt20, sum(case when TxRate='20' then TxAmt else 0 end) as TxBaseAmt20, sum(case when TxRate ='12.50' then TxBaseAmt  else 0 end) as TxAmt12, sum(case when TxRate='12.50' then TxAmt else 0 end) as TxBaseAmt12, sum(case when TxRate ='5' then TxBaseAmt  else 0 end) as TxAmt5, sum(case when TxRate='5' then TxAmt else 0 end) as TxBaseAmt5  "
    ''            ''final += " from (" + qry + ") zzzz group by FDaTe  ,todate ,Mdate ,MD ,Cust_Code ,cust_Name ,DocNo ,posting_Date ,Tin_No ,TxRate "
    ''            ''final += "  )qqqq  )LMNO group by Mdate ,MD ,Cust_Code ,cust_Name ,DocNo ,posting_Date ,Tin_No   order by MD,DocNo  "

    ''            Dim final As String = " select Mdate as Month ,DocNo  as 'Doc No',posting_Date as 'Doc Date',Tin_No as 'Buyers Tin',Cust_Name as 'Buyer Name','' as 'Inter state Branch/co asignment transfer','' as 'Export Out of India','' as 'Sale Price(excluding cst)','' as 'Central S.Tax','' as  'Total(7+8)', SUM(saleamt) as 'Sales Price(Excluding Tax)',SUM(TxAmt20) as 'AMT Taxable 20%',SUM(TxBaseAmt20) as 'DVAT 20%',SUM(TxAmt12) as 'AMT Taxable 12.50%',SUM(TxBaseAmt12) as 'DVAT 12.50%' , SUM(TxAmt5) as 'AMT Taxable 5%',SUM(TxBaseAmt5) as 'DVAT 5%',SUM(OutputTax) as 'Output Tax',SUM(total) as Total from("
    ''            final += "select Mdate ,MD ,Cust_Code ,Cust_Name ,DocNo ,posting_Date ,Tin_No ,TxRate ,(TxAmt12 +TxAmt20 +TxAmt5 ) as SalesPrice,TxAmt20 ,TxBaseAmt20 ,TxAmt12 ,TxBaseAmt12 ,TxAmt5 ,TxBaseAmt5 ,(TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5 +TxBaseAmtMann) as OutputTax,(TxAmt12 +TxAmt20 +TxAmt5+TxBaseAmt12 +TxBaseAmt20 +TxBaseAmt5 +TxBaseAmtMann+TxAmtMann) as Total ,saleamt"
    ''            final += "  from ( select FDaTe ,todate ,Mdate ,MD ,Cust_Code ,Cust_Name ,DocNo ,posting_Date ,Tin_No, TxRate ,  sum(case when TxRate ='20' then TxBaseAmt  else 0 end) as TxAmt20, sum(case when TxRate='20' then TxAmt else 0 end) as TxBaseAmt20, sum(case when TxRate ='12.50' then TxBaseAmt  else 0 end) as TxAmt12, sum(case when TxRate='12.50' then TxAmt else 0 end) as TxBaseAmt12, sum(case when TxRate ='5' then TxBaseAmt  else 0 end) as TxAmt5, sum(case when TxRate='5' then TxAmt else 0 end) as TxBaseAmt5, sum(case when TxRate ='0' then TxBaseAmt  else 0 end) as TxAmtMann ,sum(case when TxRate='0' then TxAmt else 0 end) as TxBaseAmtMann ,sum(saleamt) as saleamt  "
    ''            final += " from (" + qry + ") zzzz group by FDaTe  ,todate ,Mdate ,MD ,Cust_Code ,cust_Name ,DocNo ,posting_Date ,Tin_No ,TxRate "
    ''            final += "  )qqqq  )LMNO group by Mdate ,MD ,Cust_Code ,cust_Name ,DocNo ,posting_Date ,Tin_No   order by MD,DocNo  "



    ''            qry += " order by md"

    ''            Dim dtgv As New DataTable
    ''            dtgv = clsDBFuncationality.GetDataTable(final)
    ''            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

    ''                gv.DataSource = Nothing
    ''                gv.Rows.Clear()
    ''                gv.Columns.Clear()
    ''                gv.DataSource = dtgv
    ''                gridformat()
    ''            End If

    ''            head2 = "Summary of sales/Outward Branch Transfer Register"
    ''            head1 = "Sales For The Tax Period:  "
    ''            If refreshGrid = "F" Then

    ''                If chkexcel.Checked = True Then
    ''                    Dim arr As New List(Of String)()
    ''                    arr.Add("Annexure 2B")
    ''                    arr.Add("" + head2 + "(Month-Wise)")
    ''                    arr.Add("(See Instruction 6)")
    ''                    arr.Add("(To be filled along with return)")
    ''                    arr.Add("Name Of Dealer:  " + objCommonVar.CurrentCompanyName)
    ''                    arr.Add("" + head1 + "  From:  " + fromdate.Value + "  To: " + Todate.Value + "")
    ''                    arr.Add("Summary of Sales (As per DVAT-31)")
    ''                    Dim LocFilter As String
    ''                    Dim CustFilter As String
    ''                    If cbgCustomer.CheckedValue.Count > 0 Then
    ''                        CustFilter = clsCommon.GetMulcallString(cbgCustomer.CheckedValue)
    ''                        CustFilter = CustFilter.Replace("'", "")
    ''                        arr.Add("Customer Filter :" + CustFilter + "")
    ''                    End If
    ''                    If cbgLocation.CheckedValue.Count > 0 Then
    ''                        LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
    ''                        LocFilter = LocFilter.Replace("'", "")
    ''                        arr.Add("Loc Filter :" + LocFilter + "")
    ''                    End If
    ''                    clsCommon.MyExportToExcel(str, gv, arr, "Summary of DVAT31")

    ''                End If
    ''            End If
    ''            If refreshGrid = "F" AndAlso chkexcel.Checked = False Then
    ''                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    ''                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    ''                    Throw New Exception("No Data found to Print")
    ''                Else
    ''                    CommonServicesViewer.funreport(dt, "RptDVAT31", "DVAT-31")
    ''                End If

    ''            End If
    ''            refreshGrid = "F"
    ''        Catch ex As Exception
    ''            common.clsCommon.MyMessageBoxShow(ex.Message)
    ''        End Try
    ''    End Sub


   



    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub


    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub
    Public Sub gridformat()
        Try

            gv.MasterTemplate.SummaryRowsBottom.Clear()


            Dim summaryRowItem As New GridViewSummaryRowItem()
       
            gv.AllowAddNewRow = False

            gv.Columns("Month").IsVisible = True
            gv.Columns("Month").Width = 100
            gv.Columns("Month").HeaderText = "Month"

            gv.Columns("Doc No").IsVisible = True
            gv.Columns("Doc No").Width = 100
            gv.Columns("Doc No").HeaderText = "Doc No"


            gv.Columns("Doc Date").IsVisible = True
            gv.Columns("Doc Date").Width = 100
            gv.Columns("Doc Date").HeaderText = "Doc Date"

            gv.Columns("Buyers Tin").IsVisible = True
            gv.Columns("Buyers Tin").Width = 100
            gv.Columns("Buyers Tin").HeaderText = "Buyers Tin"

            gv.Columns("Buyer Name").IsVisible = True
            gv.Columns("Buyer Name").Width = 200
            gv.Columns("Buyer Name").HeaderText = "Buyer Name"

            gv.Columns("Inter state Branch/co asignment transfer").IsVisible = True
            gv.Columns("Inter state Branch/co asignment transfer").Width = 150
            gv.Columns("Inter state Branch/co asignment transfer").HeaderText = "Inter state Branch/co asignment transfer"


            gv.Columns("Export Out of India").IsVisible = True
            gv.Columns("Export Out of India").Width = 100
            gv.Columns("Export Out of India").HeaderText = "Export Out of India"

            gv.Columns("Sale Price(excluding cst)").IsVisible = True
            gv.Columns("Sale Price(excluding cst)").Width = 100
            gv.Columns("Sale Price(excluding cst)").HeaderText = "Sale Price(Excluding cst)"


            

            gv.Columns("Central S.Tax").IsVisible = True
            gv.Columns("Central S.Tax").Width = 100
            gv.Columns("Central S.Tax").HeaderText = "Central S.Tax"

            gv.Columns("Total(7+8)").IsVisible = True
            gv.Columns("Total(7+8)").Width = 70
            gv.Columns("Total(7+8)").HeaderText = "Total(7+8)"

            gv.Columns("Sales Price(Excluding Tax)").IsVisible = True
            gv.Columns("Sales Price(Excluding Tax)").Width = 100
            gv.Columns("Sales Price(Excluding Tax)").HeaderText = "Sales Price(Excluding Tax)"

            gv.Columns("AMT Taxable 20%").IsVisible = True
            gv.Columns("AMT Taxable 20%").Width = 100
            gv.Columns("AMT Taxable 20%").HeaderText = "AMT Taxable 20%"

            gv.Columns("DVAT 20%").IsVisible = True
            gv.Columns("DVAT 20%").Width = 100
            gv.Columns("DVAT 20%").HeaderText = "DVAT 20%"

            gv.Columns("AMT Taxable 12.50%").IsVisible = True
            gv.Columns("AMT Taxable 12.50%").Width = 100
            gv.Columns("AMT Taxable 12.50%").HeaderText = "AMT Taxable 12.50%"

            gv.Columns("DVAT 12.50%").IsVisible = True
            gv.Columns("DVAT 12.50%").Width = 100
            gv.Columns("DVAT 12.50%").HeaderText = "DVAT 12.50%"

            gv.Columns("AMT Taxable 5%").IsVisible = True
            gv.Columns("AMT Taxable 5%").Width = 100
            gv.Columns("AMT Taxable 5%").HeaderText = "AMT Taxable 5%"

            gv.Columns("DVAT 5%").IsVisible = True
            gv.Columns("DVAT 5%").Width = 100
            gv.Columns("DVAT 5%").HeaderText = "DVAT 5%"

            gv.Columns("Output Tax").IsVisible = True
            gv.Columns("Output Tax").Width = 100
            gv.Columns("Output Tax").HeaderText = "Output Tax"

            gv.Columns("Total").IsVisible = True
            gv.Columns("Total").Width = 100
            gv.Columns("Total").HeaderText = "Total"


            Dim SumSaleTAx As New GridViewSummaryItem("Sales Price(Excluding Tax)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumSaleTAx)
            Dim SumAmtTAx20 As New GridViewSummaryItem("AMT Taxable 20%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumAmtTAx20)
            Dim SumDvat20 As New GridViewSummaryItem("DVAT 20%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumDvat20)
            Dim SumAmtTAx12 As New GridViewSummaryItem("AMT Taxable 12.50%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumAmtTAx12)
            Dim SumDvat12 As New GridViewSummaryItem("DVAT 12.50%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumDvat12)

            Dim SumAmtTAx5 As New GridViewSummaryItem("AMT Taxable 5%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumAmtTAx5)
            Dim SumDvat5 As New GridViewSummaryItem("DVAT 5%", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumDvat5)

            Dim SumOutputTax As New GridViewSummaryItem("Output Tax", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumOutputTax)
            Dim SumTotal As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTotal)


            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        printcrystal()
    End Sub

    Private Sub btnrefresh_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        refreshGrid = "T"
        print()
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        printdataExport(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        printdataExport(EnumExportTo.PDF)
    End Sub

   
End Class
