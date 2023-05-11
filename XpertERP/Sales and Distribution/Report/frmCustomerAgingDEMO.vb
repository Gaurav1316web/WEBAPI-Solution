Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
'---Created Bby --Pankaj Kumar Chaudhary....
'---Updation By --- [Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000000771]

Public Class FrmCustomerAgingDEMO
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmCustomerAgeing_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub FrmCustomerAgeing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocationCode()
        LoadCustomerGroup()
        chkCGAll.IsChecked = True
        chkLOcAll.IsChecked = True
        chkType.Checked = True
        dtpAgeof.Value = Date.Today
        dtpCutoffDate.Value = Date.Today
        chkCustomerAll.IsChecked = True
        cbgCustomer.Enabled = False
        ddlAgedRcvbl.Text = "Aged Trial Balance By Due Date"
        chkActive.Checked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        txt5.Text = ""
        txt6.Text = ""
        txt7.Text = ""
        txt8.Text = ""
        txtOvr.Text = ""
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCustomerAgeing)
        If Not (MyBase.isReadFlag) Then
            RadMessageBox.Show("Permission Denied")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Cust_Group_Code as [Customer Group] from TSPL_CUSTOMER_MASTER "
        If chkActive.Checked Then
            qry += " WHERE Status='N'"
        ElseIf chkInactive.Checked Then
            qry += " WHERE Status='Y'"
        ElseIf chkAll.Checked Then
            qry += ""
        End If
        qry += "  order by  Cust_Code "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub

    Private Sub LoadCustomerGroup()
        Dim qry As String = "Select Cust_Group_Code as Code, Cust_Group_Desc as Description from TSPL_CUSTOMER_GROUP_MASTER"
        cbgCustomerGroup.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomerGroup.ValueMember = "Code"
        cbgCustomerGroup.DisplayMember = "Description"
    End Sub
    
    Private Sub LoadLocationCode()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub

    Sub print()
        Try
            Dim txt1 As String = Me.txt1.Text
            Dim txt2 As String = Me.txt2.Text
            Dim txt3 As String = Me.txt3.Text
            Dim txt4 As String = Me.txt4.Text
            Dim txt5 As String = Me.txt5.Text
            Dim txt6 As String = Me.txt6.Text
            Dim txt7 As String = Me.txt7.Text
            Dim txt8 As String = Me.txt8.Text
            Dim txtOvr As String
            Dim strNo As String
            Dim type As String = Me.ddlAgedRcvbl.Text
            Dim strTtpe As String = ""
            Dim IsFifoBased As String = "N"

            If chkType.Checked = True Then
                strTtpe = "SMry"
            End If
            If chkFifo.Checked Then
                IsFifoBased = "Y"
            End If
            Dim ArryLst As New ArrayList
            ArryLst.Add("IN")
            ArryLst.Add("DB")
            ArryLst.Add("CR")
            ArryLst.Add("RC")
            ArryLst.Add("AV")
            ArryLst.Add("OA")
            ArryLst.Add("UC")
            ArryLst.Add("SR")
            ArryLst.Add("VGCL")
            ArryLst.Add("AD")
            ArryLst.Add("RF")
            ArryLst.Add("RC")

            If ArryLst.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Transaction Type")
                Return
            End If

            If Me.txt1.Text = "" Or Me.txt2.Text = "" Or Me.txt3.Text = "" Then
                MsgBox("Select Atleast 3 Buckets!", MsgBoxStyle.Information, "Aged Trial Balance Report")
                Exit Sub
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text = "" And Me.txt5.Text = "" And Me.txt6.Text = "" And Me.txt7.Text = "" And Me.txt8.Text = "" Then
                strNo = 0
                txtOvr = Me.txt3.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text = "" And Me.txt6.Text = "" And Me.txt7.Text = "" And Me.txt8.Text = "" Then
                strNo = 1
                txtOvr = Me.txt4.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text <> "" And Me.txt6.Text = "" And Me.txt7.Text = "" And Me.txt8.Text = "" Then
                strNo = 2
                txtOvr = Me.txt5.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text <> "" And Me.txt6.Text <> "" And Me.txt7.Text = "" And Me.txt8.Text = "" Then
                strNo = 3
                txtOvr = Me.txt6.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text <> "" And Me.txt6.Text <> "" And Me.txt7.Text <> "" And Me.txt8.Text = "" Then
                strNo = 4
                txtOvr = Me.txt7.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text <> "" And Me.txt6.Text <> "" And Me.txt7.Text <> "" And Me.txt8.Text <> "" Then
                strNo = ""
                txtOvr = Me.txtOvr.Text
            Else
                MsgBox("Selection Criteria Not In Order", MsgBoxStyle.Information, "Aged Trial Balance Report")
                Exit Sub
            End If

            Dim CheckCustomer As String = ""
            If chkActive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='N'"
            ElseIf chkInactive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='Y'"
            End If

            Dim rptHeading As String
            rptHeading = "Aged Trial Balance Report"

            Dim StrQuery As String
            Dim strEmptyQry As String = ""
            Dim strFilledQry As String = ""
            Dim strUpperQry As String = ""
            Dim strUpperQry1 As String = ""
            Dim strInnerQry As String = ""
            Dim strLowerQry As String = ""
            Dim strLowerQry1 As String = ""

            If ddlAgedRcvbl.Text = "Aged Trial Balance By Document date" Then

                strEmptyQry = " SELECT   TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name],Document_No as [Document Id] ,Description as [Desc] , Empty_Value*-1 AS [Due Amount], CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date], DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days, 'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 " + CheckCustomer + " " & _
                      " UNION ALL SELECT   TSPL_ADJUSTMENT_HEADER.Comp_Code,TSPL_ADJUSTMENT_HEADER.Customer_CODE,'',TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Adjustment_No,'',case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DueDate,'',convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'AD',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=TSPL_ADJUSTMENT_HEADER.Loc_Code) FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')=''  and TSPL_ADJUSTMENT_HEADER.Posted='Y' " + CheckCustomer + " "
                strFilledQry = " SELECT   TSPL_Customer_Invoice_Head.Comp_Code,TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," & _
                       " TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Document Id],TSPL_Customer_Invoice_Head.Description as [Desc],case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end as [Due Amount],  TSPL_Customer_Invoice_Head.due_date as [Due Date]," & _
                       " '' AS type, CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date],  DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days , case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] ," & _
                       "  TSPL_Customer_Invoice_Head.Loc_Code as Location FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No where TSPL_Customer_Invoice_Head.Status='1' " + CheckCustomer + " " & _
                       " UNION ALL SELECT  TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name] ,Document_No as [Document Id]  ,Description as [Desc] ,(Total_Order_Amt)*-1 as [Due Amount] , CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date]  , DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 " + CheckCustomer + " " & _
                       " union All select  TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 " + CheckCustomer + " " & _
                       " UNION ALL select  TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' " + CheckCustomer + " " & _
                       " union All select  TSPL_RECEIPT_HEADER.Comp_Code ,TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_RECEIPT_HEADER.Customer_Name ,TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_HEADER.Entry_Desc , Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) ,'' AS type ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) , DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" + dtpCutoffDate.Value + "',103)), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as Location  from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N' AND TSPL_RECEIPT_HEADER.Balance_Amt>0 " + CheckCustomer + " " & _
                       " UNION ALL Select  TSPL_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], Adjustment_No as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], Adjustment_Amount*-1 as [Due Amount], CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SALE_INVOICE_HEAD.Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' " + CheckCustomer + " "

            ElseIf ddlAgedRcvbl.Text = "Aged Trial Balance By Due Date" Then

                strEmptyQry = " select TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name ,Document_No  ,Description , Empty_Value*-1 AS [Due Amount], CONVERT(DATE,Document_Date,103) as [Due Date] ,'', CONVERT(Date,Document_Date,103) as [Document Date] , DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'SR',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location)  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 " + CheckCustomer + " " & _
                                " UNION ALL SELECT TSPL_ADJUSTMENT_HEADER.Comp_Code, TSPL_ADJUSTMENT_HEADER.Customer_CODE,'',TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Adjustment_No,'',case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0)) FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) End AS [Due Amount],convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DueDate,'',convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'AD',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=TSPL_ADJUSTMENT_HEADER.Loc_Code) FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')='' and TSPL_ADJUSTMENT_HEADER.Posted='Y' " + CheckCustomer + " "
                strFilledQry = " SELECT  TSPL_Customer_Invoice_Head.Comp_Code, TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," & _
                           " TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Document Id],TSPL_Customer_Invoice_Head.Description as [Desc],case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end as [Due Amount],  TSPL_Customer_Invoice_Head.due_date as [Due Date]," & _
                           " '' AS type, CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date],  DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.due_date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days , case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] ," & _
                           " RIGHT(TSPL_Customer_Invoice_Head.Customer_Control_AC,3) as Location FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No where TSPL_Customer_Invoice_Head.Status='1' " + CheckCustomer + " " & _
                            "  UNION ALL select TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code ,Parent_Customer_No ,Cust_Name ,Document_No  ,Description ,(Total_Order_Amt)*-1 AS [AMT] , CONVERT(DATE,Document_Date,103) as [Due Date] ,'', CONVERT(Date,Document_Date,103) as [Document Date], DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'SR',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location)  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 " + CheckCustomer + " " & _
                            " union All select TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 " + CheckCustomer + " " & _
                            " UNION ALL select TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name  as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' " + CheckCustomer + " " & _
                            " union All select   TSPL_RECEIPT_HEADER.Comp_Code , TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_RECEIPT_HEADER.Customer_Name ,TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_HEADER.Entry_Desc , Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) ,'' AS type ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) , DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" + dtpCutoffDate.Value + "',103)), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as Location from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M')  and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N'  AND TSPL_RECEIPT_HEADER.Balance_Amt>0 " + CheckCustomer + " " & _
                            " UNION ALL Select TSPL_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], Adjustment_No as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], Adjustment_Amount*-1 as [Due Amount], CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SALE_INVOICE_HEAD.Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' " + CheckCustomer + " "
            End If

            strInnerQry = strFilledQry + " Union All " + strEmptyQry

            strUpperQry = " select '" + rptHeading + "' as rptHeading, '" + txtCurr.Text + "' AS First_Period, '" + Me.txt1.Text + "' AS Second_Period, '" + Me.txt2.Text + "' AS [Third Period], '" + Me.txt3.Text + "' AS [Fourth Period], '" + Me.txt4.Text + "' AS [Fifth Period],'" + Me.txt5.Text + "' AS [Sixth Period]," & _
                                " '" + Me.txt6.Text + "' AS [Seventh Period],'" + Me.txt7.Text + "' AS [Eight Period], '" + Me.txt8.Text + "' AS [Nineth Period], '" + txtOvr + "' AS [Over], Query.*, '' AS From_Vendor, '' AS To_Vendor, " & _
                                " '" + Me.ddlAgedRcvbl.Text + "' AS Report_Type,  '" + Me.dtpAgeof.Value + "' AS AgeofDate,'" + strTtpe + "' as [Summary], '" + IsFifoBased + "' as [IsFifoBased], TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc," & _
                                " TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Add1+case  when isnull(TSPL_COMPANY_MASTER.Add2,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add2 +case  when isnull(TSPL_COMPANY_MASTER.Add3,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 end end as comp_address " & _
                                " from ( "
            strUpperQry1 = " Select MAX(Comp_Code) AS Comp_Code, [Customer Id], MAX([Parent Code]) AS [Parent Code], MAX([Customer Name]) AS [Customer Name], [Document Id], MAX([Desc]) as [Desc], SUM([Due Amount]) AS [Due Amount], MAX([Due Date]) AS [Due Date], MAX(type) AS type, MAX([Document Date]) AS [Document Date], MAX(Ageing_Days) AS Ageing_Days, MAX(Document_Type) AS Document_Type, MAX(Location) AS Location  from ( "

            strLowerQry = " ) XXX " & _
                          " where  XXX.Document_Type in (" + clsCommon.GetMulcallString(ArryLst) + "  ) " & _
                          " and convert(date,XXX.[Document Date] ,103) <= convert(date,'" + dtpCutoffDate.Value + "',103)"

            If chkCustomerSelect.IsChecked = True Then
                If cbgCustomer.CheckedValue.Count <= 0 Then
                    RadMessageBox.Show("Please Select Atleast One Customer.")
                    Return
                End If
                strLowerQry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            
            If chkLOcSelect.IsChecked = True Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    RadMessageBox.Show("Please Select Atleast One Location Code.")
                    Return
                End If
                strLowerQry += " and XXX.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            strLowerQry += " AND [Due Amount] <> 0 "
            strLowerQry += " Group By XXX.[Customer Id], XXX.[Document Id] "
            strLowerQry1 = " ) Query " & _
                           " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON Query.[Customer Id]=TSPL_CUSTOMER_MASTER.Cust_Code" & _
                           " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code = Query .Comp_Code " & _
                           " Where 1=1 "
            If chkCGSelect.IsChecked = True Then
                If cbgCustomerGroup.CheckedValue.Count <= 0 Then
                    RadMessageBox.Show("Please Select Atleast One Customer Group.")
                    Return
                End If
                strLowerQry1 += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustomerGroup.CheckedValue) + ") "
            End If
            Dim dt As New DataTable
            If chkFifo.Checked Then
                StrQuery = strUpperQry + strUpperQry1 + strInnerQry + strLowerQry + strLowerQry1 + "AND 1=2"
                dt = clsDBFuncationality.GetDataTable(StrQuery)
                Dim dtCustomer As DataTable = clsDBFuncationality.GetDataTable("Select Distinct [Customer Id] from ( " + strInnerQry + " ) Customer Where [Due Amount]<>0 ")
                For Each drCustomer As DataRow In dtCustomer.Rows
                    '--------------------FIFO(-ve balance)-------------------
                    Dim strFifoQry As String = strUpperQry + strUpperQry1 + strInnerQry + strLowerQry + strLowerQry1
                    strFifoQry += " and Query.[Customer Id] = '" + clsCommon.myCstr(drCustomer("Customer Id")) + "'"
                    strFifoQry += " AND [Due Amount] < 0 "
                    Dim dtFifo As DataTable = clsDBFuncationality.GetDataTable(strFifoQry)
                    '--------------------------------------------------------
                    '--------------------FIFO(+ve balance)-------------------
                    Dim strFifoQry1 As String = strUpperQry + strUpperQry1 + strInnerQry + strLowerQry + strLowerQry1
                    strFifoQry1 += " and Query.[Customer Id] = '" + clsCommon.myCstr(drCustomer("Customer Id")) + "'"
                    strFifoQry1 += " AND [Due Amount] > 0 "
                    Dim dtFifo1 As DataTable = clsDBFuncationality.GetDataTable(strFifoQry1)
                    '--------------------------------------------------------
                    If dtFifo1.Rows.Count <= 0 And dtFifo.Rows.Count > 0 Then
                        For Each dr As DataRow In dtFifo.Rows
                            Dim dRow As DataRow = dt.NewRow()
                            dRow.ItemArray = dr.ItemArray
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    If dtFifo1.Rows.Count > 0 And dtFifo.Rows.Count <= 0 Then
                        For Each dr As DataRow In dtFifo1.Rows
                            Dim dRow As DataRow = dt.NewRow()
                            dRow.ItemArray = dr.ItemArray
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    If dtFifo1.Rows.Count > 0 And dtFifo.Rows.Count > 0 Then
                        Dim NegativeAmt As Double = Math.Round(clsCommon.myCdbl(dtFifo.Compute("Sum([Due Amount])", "") * -1), 0)
                        Dim PositiveAmt As Double = Math.Round(clsCommon.myCdbl(dtFifo1.Compute("Sum([Due Amount])", "")), 0)
                        If NegativeAmt > PositiveAmt Then
                            Dim AppliedAmt As Double = clsCommon.myCdbl(dtFifo1.Compute("Sum([Due Amount])", ""))
                            For Each dr As DataRow In dtFifo.Rows
                                If AppliedAmt > 0 Then
                                    If (clsCommon.myCdbl(dr("Due Amount")) * -1) <= AppliedAmt Then
                                        AppliedAmt = AppliedAmt + clsCommon.myCdbl(dr("Due Amount"))
                                    Else
                                        dr.Item("Due Amount") = clsCommon.myCdbl(dr("Due Amount")) + AppliedAmt
                                        AppliedAmt = 0
                                        Dim dRow As DataRow = dt.NewRow()
                                        dRow.ItemArray = dr.ItemArray
                                        dt.Rows.Add(dRow)
                                    End If
                                Else
                                    Dim dRow As DataRow = dt.NewRow()
                                    dRow.ItemArray = dr.ItemArray
                                    dt.Rows.Add(dRow)
                                End If
                            Next
                        ElseIf NegativeAmt < PositiveAmt Then
                            Dim AppliedAmt As Double = clsCommon.myCdbl(dtFifo.Compute("Sum([Due Amount])", "") * -1)
                            For Each dr As DataRow In dtFifo1.Rows
                                If AppliedAmt > 0 Then
                                    If clsCommon.myCdbl(dr("Due Amount")) <= AppliedAmt Then
                                        AppliedAmt = AppliedAmt - clsCommon.myCdbl(dr("Due Amount"))
                                    Else
                                        dr.Item("Due Amount") = clsCommon.myCdbl(dr("Due Amount")) - AppliedAmt
                                        AppliedAmt = 0
                                        Dim dRow As DataRow = dt.NewRow()
                                        dRow.ItemArray = dr.ItemArray
                                        dt.Rows.Add(dRow)
                                    End If
                                Else
                                    Dim dRow As DataRow = dt.NewRow()
                                    dRow.ItemArray = dr.ItemArray
                                    dt.Rows.Add(dRow)
                                End If
                            Next
                        End If

                    End If
                Next
            Else
                StrQuery = strUpperQry + strUpperQry1 + strInnerQry + strLowerQry + strLowerQry1
                If ddlAgedRcvbl.Text = "Aged Trial Balance By Due Date" Then
                    StrQuery += " order by Query.[Due Date]"
                Else
                    StrQuery += " order by Query.[Document Date]"
                End If
                dt = clsDBFuncationality.GetDataTable(StrQuery)
            End If


            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptAgedTrialBalance" + strNo + "", "A/R Customer Ageing Report")
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub chkVendorSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub
    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        ' cbgCustomer.Enabled = Not chkCustomerSelect.IsChecked
    End Sub

    Private Sub txt1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt1.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt2.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt3.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt4.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt5.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt6.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt7_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt7.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt8.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub

    Private Sub txt8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt8.TextChanged
        Me.txtOvr.Text = Me.txt8.Text
    End Sub

    Private Sub chkCusSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLOcAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLOcAll.IsChecked
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        LoadCustomer()
    End Sub

    Private Sub chkInactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInactive.CheckedChanged
        LoadCustomer()
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        LoadCustomer()
    End Sub

    Private Sub chkCGAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCGAll.ToggleStateChanged
        cbgCustomerGroup.Enabled = False
    End Sub

    Private Sub chkCGSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCGSelect.ToggleStateChanged
        cbgCustomerGroup.Enabled = True
    End Sub


    Private Sub chkType_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkType.ToggleStateChanged
        chkFifo.Checked = False
        If chkType.Checked Then
            chkFifo.Enabled = False
        Else
            chkFifo.Enabled = True
        End If
    End Sub

End Class
