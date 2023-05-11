Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net

Public Class FrmCustomerOutstanding
    Inherits FrmMainTranScreen

  
    Private Sub FrmCustomerOutstanding_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadCustomer()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where Email <> ''"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnCustomerLedger)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

    End Sub
    Sub print()
        
        'Try
        '    If cbgCustomer.CheckedValue.Count <= 0 Then
        '        common.clsCommon.MyMessageBoxShow("Please select at least one Customer")
        '        Return
        '    End If
        '    ' Dim qry As String
        '    Dim strTtpe As String = ""


        '    Dim ArryLst As New ArrayList
        '    ArryLst.Add("IN")
        '    ArryLst.Add("DB")
        '    ArryLst.Add("CR")
        '    ArryLst.Add("RC")
        '    ArryLst.Add("AV")
        '    ArryLst.Add("OA")
        '    ArryLst.Add("UC")
        '    ArryLst.Add("SR")
        '    ArryLst.Add("VGCL")
        '    ArryLst.Add("AD")
        '    ArryLst.Add("RF")
        '    ArryLst.Add("RC")


        '    Dim FromDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        '    Dim BaseQry As String = " select sum([Due Amount]) as EffectiveAmt,Cust_Code as CustCode,[Customer Name] as CustName from (SELECT  TSPL_Customer_Invoice_Head.Comp_Code, TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," & _
        '                   " TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name],   TSPL_Customer_Invoice_Head.Document_No  as [Document Id],TSPL_Customer_Invoice_Head.Description as [Desc],case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Balance_Amt  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Balance_Amt  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Balance_Amt*-1  end  +(SELECT ISNULL(SUM(UnApplied_Balance),0) FROM dbo.TSPL_RECEIPT_DETAIL INNER JOIN dbo.TSPL_RECEIPT_HEADER ON dbo.TSPL_RECEIPT_DETAIL.Receipt_No=dbo.TSPL_RECEIPT_HEADER.Receipt_No WHERE dbo.TSPL_RECEIPT_HEADER.Posted='N' AND   TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No)   as [Due Amount],  TSPL_Customer_Invoice_Head.due_date as [Due Date]," & _
        '                   " '' AS type, CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date],   case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] ," & _
        '                   " RIGHT(TSPL_Customer_Invoice_Head.Customer_Control_AC,3) as Location FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_Customer_Invoice_Head.Status='1'  " & _
        '                   " union all  select  TSPL_SALE_INVOICE_HEAD.Comp_Code, TSPL_SALE_INVOICE_HEAD.Cust_Code ,Parent_Customer_No ,Cust_Name ,Sale_Invoice_No ,Description ,Balance_Amt,Due_Date ,'',Sale_Invoice_Date , 'IN',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location)  from TSPL_SALE_INVOICE_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_SALE_INVOICE_HEAD.Balance_Amt>0  and  TSPL_SALE_INVOICE_HEAD.Is_Post='Y'  " & _
        '                    "  UNION ALL select  TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code ,Parent_Customer_No ,Cust_Name ,Document_No  ,Description ,(Total_Order_Amt +Empty_Value)*-1 AS [AMT] , CONVERT(DATE,Document_Date,103) as [Due Date] ,'', CONVERT(DATE,Document_Date,103) as [Document Date]  ,'SR',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location)  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 " & _
        '                    " union All select  TSPL_VCGL_Head.Comp_Code,  TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1  " & _
        '                    " UNION ALL select TSPL_VCGL_Head.Comp_Code,  TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name  as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' " & _
        '                    " UNION ALL SELECT  TSPL_ADJUSTMENT_HEADER.Comp_Code,  TSPL_ADJUSTMENT_HEADER.Customer_CODE,'',TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Adjustment_No,'',case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DueDate,'',convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),'AD',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=TSPL_ADJUSTMENT_HEADER.Loc_Code) FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE ( (TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND (TSPL_ADJUSTMENT_HEADER.Document_No= '' and TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '')   )  and TSPL_ADJUSTMENT_HEADER.Posted='Y'  " & _
        '                    " union All select  TSPL_RECEIPT_HEADER.Comp_Code,  TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_RECEIPT_HEADER.Customer_Name ,TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_HEADER.Entry_Desc ," & _
        '                   " Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) ,'' AS type ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) , " & _
        '                   " case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' end , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as Location from TSPL_RECEIPT_HEADER inner join " & _
        '                   " TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('R','M')  and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N'  AND TSPL_RECEIPT_HEADER.Balance_Amt>0 " & _
        '                   " ) Query " & _
        '                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON Query.[Customer Id]=TSPL_CUSTOMER_MASTER.Cust_Code" & _
        '                " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code        LEFT OUTER JOIN TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code = Query .Comp_Code " & _
        '                " where  query.Document_Type in (" + clsCommon.GetMulcallString(ArryLst) + "  ) " & _
        '               " and convert(date,Query.[Document Date] ,103) <= convert(date,'" + txtToDate.Value + "',103) and " & _
        '               " Cust_Code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") group by Cust_Code ,[Customer Name]"
        '    Dim StrQuery As String = BaseQry



        '    Dim strEmail As String
        '    Dim strCust, strcustName As String
        '    Dim decAmt As Decimal
        '    Dim dt As DataTable


        '    dt = clsDBFuncationality.GetDataTable(StrQuery)

        '    If Process.GetProcessesByName("OutLook").Length < 1 Then
        '        'restarts the Process
        '        Process.Start("OutLook.exe")
        '    End If
        '    Dim oApp As New Outlook.Application()
        '    Dim oMsg As Outlook.MailItem
        '    Dim blnmail As Boolean = False
        '    For Each dr As DataRow In dt.Rows
        '        decAmt = 0
        '        strCust = clsCommon.myCstr(dr("CustCode"))
        '        strcustName = clsCommon.myCstr(dr("Custname"))
        '        decAmt = clsCommon.myCdbl(dr("EffectiveAmt"))
        '        If decAmt > 0 Then
        '            oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
        '            strEmail = clsDBFuncationality.getSingleValue("select Email,Cust_Code from TSPL_CUSTOMER_MASTER where Cust_Code ='" + strCust + "' ")
        '            oMsg.Body = "Hello  " & strcustName & "  your Rs. " & decAmt & "  outstanding amount is due as on  " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
        '            oMsg.Subject = "Outdtanding Amount"
        '            oMsg.Recipients.Add(strEmail)
        '            oMsg.Send()
        '            blnmail = True
        '        End If

        '    Next
        '    If blnmail = True Then
        '        RadMessageBox.Show("Mail sent successfully")
        '    End If
        '    oMsg = Nothing
        '    oApp = Nothing
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub SplitContainer1_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
End Class
