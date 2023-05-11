Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
'--Updation By Pankaj Kumar [BM00000000498, BM00000000499, BM00000000602, BM00000000652,BM00000000666, BM00000000876, BM00000001280, BM00000001289, BM00000001721]
Public Class FrmCustomerAgeing
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
        LoadParentCode()
        LoadLocationCode()
        LoadCustomerGroup()
        chkCGAll.IsChecked = True
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()

        chkParentAll.IsChecked = True
        chkInvoice.Checked = True
        chkLOcAll.IsChecked = True
        chkCreditNote.Checked = True
        chkDebitNote.Checked = True
        chkReceipt.Visible = True
        chkAdvance.Checked = True
        chkOnAccount.Checked = True
        chkType.Checked = True
        chkReceipt.Checked = True
        dtpAgeof.Value = Date.Today
        dtpCutoffDate.Value = Date.Today
        chkCustomerAll.IsChecked = True
        cbgCustomer.Enabled = False
        ddlAgedRcvbl.Text = "Aged Trial Balance By Due Date"
        cmbType.Text = "Both"
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        chkActive.Checked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        txt5.Text = ""
        txt6.Text = ""
        txt7.Text = ""
        txt8.Text = ""
        txtOvr.Text = ""
        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "VIzag") = CompairStringResult.Equal Then
        '    chkFifo.Visible = True
        'Else
        '    chkFifo.Visible = False
        'End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCustomerAgeing)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")

        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Cust_Group_Code as [Customer Group] from TSPL_CUSTOMER_MASTER "
        If chkActive.Checked Then
            qry += " WHERE Status='N'"
            If chkSettlementPending.Checked Then
                qry += " AND OnHold='Y'"
            Else
                qry += " AND OnHold='N'"
            End If
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
    Private Sub LoadParentCode()
        Dim qry As String = "select distinct Parent_Customer_No as [Parent Code]from TSPL_CUSTOMER_MASTER where Parent_Customer_No <> '' "
        cbgParentCode.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgParentCode.ValueMember = "Parent Code"
        cbgParentCode.DisplayMember = "Parent Code"
    End Sub
    Private Sub LoadLocationCode()
        ' Dim qry As String = "   select Location_Code as [Location Code],Location_Desc as [Location Name]  from TSPL_LOCATION_MASTER where Location_Type ='Physical' "
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
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
            If chkInvoice.Checked = True Then
                ArryLst.Add("IN")
            End If
            If chkDebitNote.Checked = True Then
                ArryLst.Add("DB")
            End If
            If chkCreditNote.Checked = True Then
                ArryLst.Add("CR")
            End If
            If chkReceipt.Checked = True Then
                ArryLst.Add("RC")
            End If
            If chkAdvance.Checked = True Then
                ArryLst.Add("AV")
            End If
            If chkOnAccount.Checked = True Then
                ArryLst.Add("OA")
            End If
            ArryLst.Add("UC")
            ArryLst.Add("SR")
            ArryLst.Add("VGCL")
            ArryLst.Add("AD")
            ArryLst.Add("RF")
            ArryLst.Add("RC")
            ArryLst.Add("EX-ADJ")
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
                If chkSettlementPending.Checked Then
                    CheckCustomer += " AND TSPL_CUSTOMER_MASTER.OnHold='Y'"
                Else
                    CheckCustomer += " AND TSPL_CUSTOMER_MASTER.OnHold='N'"
                End If
            ElseIf chkInactive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='Y'"
            End If

            Dim rptHeading As String
            If clsCommon.CompairString(cmbType.Text, "Empty") = CompairStringResult.Equal Then
                rptHeading = "Aged Trial Balance Report (Empty)"
            ElseIf clsCommon.CompairString(cmbType.Text, "Filled") = CompairStringResult.Equal Then
                rptHeading = "Aged Trial Balance Report (Cash)"
            Else
                rptHeading = "Aged Trial Balance Report"
            End If


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
                      " union all SELECT * FROM ( select  TSPL_SALE_INVOICE_HEAD.Comp_Code, TSPL_SALE_INVOICE_HEAD.Cust_Code ,Parent_Customer_No ,Cust_Name ,Sale_Invoice_No ,Description , (TSPL_SALE_INVOICE_HEAD.Empty_Value+(Select ISNULL(SUM(DueAmt),0) from (Select TSPL_ADJUSTMENT_HEADER.Document_No , case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0)) FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_HEADER.Adjustment_No) END as DueAmt from TSPL_ADJUSTMENT_HEADER Where TSPL_ADJUSTMENT_HEADER.Posted='Y' AND CONVERT(Date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=Convert(Date,'" + Me.dtpAgeof.Value + "',103)) XXX Where Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No )-(Select ISNULL(SUM(TSPL_SALE_RETURN_HEAD.Empty_Value ),0) from TSPL_SALE_RETURN_HEAD WHERE TSPL_SALE_RETURN_HEAD.Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AND TSPL_SALE_RETURN_HEAD.Is_Post='Y' AND Convert(Date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<=Convert(Date,'" + Me.dtpAgeof.Value + "',103))) AS [Due Amount], Due_Date ,'' as [type], Sale_Invoice_Date , DATEDIFF(day,convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'IN' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) AS Location from TSPL_SALE_INVOICE_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_SALE_INVOICE_HEAD.Is_Post='Y' " + CheckCustomer + " ) XXX WHERE [Due Amount]<>0 " & _
                      " UNION ALL SELECT TSPL_ADJUSTMENT_HEADER.Comp_Code,TSPL_ADJUSTMENT_HEADER.Customer_CODE,'',TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Adjustment_No,'',case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DueDate,'',convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'AD',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=TSPL_ADJUSTMENT_HEADER.Loc_Code) FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')=''  and TSPL_ADJUSTMENT_HEADER.Posted='Y' " + CheckCustomer + " " & _
                      " UNION ALL SELECT TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'21/10/2013',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 AND  TSPL_VCGL_Head.Is_Empty=1 " + CheckCustomer + " " & _
                      " UNION ALL select  TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'', Case When TSPL_VCGL_Head.Is_Empty =1 Then (CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END) Else 0 End As DueAmt ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' " + CheckCustomer + "" & _
                      " UNION ALL Select TSPL_EXPIRY_HEADER.Comp_Code, Customer_CODE, TSPL_CUSTOMER_MASTER.Parent_Customer_No, TSPL_CUSTOMER_MASTER.Customer_NAME, Document_No, Description, TSPL_JOURNAL_MASTER.Total_Credit_Amt*-1 as DueAmt, Document_Date as DueDate, '' as Type, Document_Date, DATEDIFF(day, TSPL_EXPIRY_HEADER.Document_Date, convert(date,'" + Me.dtpAgeof.Value + "',103)) as AgeingDays, 'EX-ADJ', TSPL_EXPIRY_HEADER.Loc_Code from TSPL_EXPIRY_HEADER LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_EXPIRY_HEADER.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_EXPIRY_HEADER.Customer_CODE WHERE TSPL_EXPIRY_HEADER.Posted='Y' " + CheckCustomer + " "
                strFilledQry = " SELECT TSPL_Customer_Invoice_Head.Comp_Code,TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," & _
                       " TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name],   TSPL_Customer_Invoice_Head.Document_No  as [Document Id],TSPL_Customer_Invoice_Head.Description as [Desc],case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end as [Due Amount],  TSPL_Customer_Invoice_Head.due_date as [Due Date]," & _
                       " '' AS type, CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date],  DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days , case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] ," & _
                       "  RIGHT(TSPL_Customer_Invoice_Head.Customer_Control_AC,3) as Location FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_Customer_Invoice_Head.Status='1' " + CheckCustomer + " " & _
                       " UNION ALL SELECT  TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name] ,Document_No as [Document Id]  ,Description as [Desc] ,(Total_Order_Amt)*-1 as [Due Amount] , CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date]  , DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 " + CheckCustomer + " " & _
                       " union all SELECT * From ( select  TSPL_SALE_INVOICE_HEAD.Comp_Code,TSPL_SALE_INVOICE_HEAD.Cust_Code ,Parent_Customer_No ,Cust_Name ,Sale_Invoice_No ,Description , Total_Invoice_Amt-(Select ISNULL(SUM(Total_Invoice_Amt),0) from TSPL_SALE_RETURN_HEAD WHERE Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AND TSPL_SALE_RETURN_HEAD.Is_Post='Y' AND Convert(Date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<=Convert(Date,'" + Me.dtpAgeof.Value + "',103)) as [Due Amount] ,Due_Date ,'' as [Type],Sale_Invoice_Date , DATEDIFF(day,convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'IN' as [Document_Type],(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) As Location from TSPL_SALE_INVOICE_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_SALE_INVOICE_HEAD.Is_Post='Y' " + CheckCustomer + " ) XXX Where [Due Amount]<>0 " & _
                       " union All select  TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 AND TSPL_VCGL_Head.Is_Empty <> 1 " + CheckCustomer + " " & _
                       " UNION ALL select  TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'', Case When TSPL_VCGL_Head.Is_Empty <> 1 Then (CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END) Else 0 End As DueAmt ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' " + CheckCustomer + " " & _
                       " union All select  TSPL_RECEIPT_HEADER.Comp_Code ,TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_RECEIPT_HEADER.Customer_Name ,TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_HEADER.Entry_Desc , Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else TSPL_RECEIPT_HEADER.Receipt_Amount*-1 End ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) ,'' AS type ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) , DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" + dtpCutoffDate.Value + "',103)), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as Location  from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M') and TSPL_RECEIPT_HEADER.Posted='Y' AND Not exists(Select 1 from TSPL_BANK_REVERSE Where Reverse_Document='Receipts' AND Document_No=TSPL_RECEIPT_HEADER.Receipt_No AND Post='P' AND Reversal_Date<=Convert(Date,'" + Me.dtpAgeof.Value + "',103)) " + CheckCustomer + " " & _
                       " UNION ALL Select  TSPL_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], Adjustment_No as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], Adjustment_Amount*-1 as [Due Amount], CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SALE_INVOICE_HEAD.Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' " + CheckCustomer + " "

            ElseIf ddlAgedRcvbl.Text = "Aged Trial Balance By Due Date" Then

                strEmptyQry = " SELECT * FROM ( SELECT  TSPL_SALE_INVOICE_HEAD.Comp_Code,TSPL_SALE_INVOICE_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code], Cust_Name AS [Customer Name] ,Sale_Invoice_No as [Document Id] ,Description as [Desc] , (TSPL_SALE_INVOICE_HEAD.Empty_Value+(Select ISNULL(SUM(DueAmt),0) from (Select TSPL_ADJUSTMENT_HEADER.Document_No , case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0)) FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_HEADER.Adjustment_No) END as DueAmt from TSPL_ADJUSTMENT_HEADER Where TSPL_ADJUSTMENT_HEADER.Posted='Y' AND CONVERT(Date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=Convert(Date,'" + Me.dtpAgeof.Value + "',103)) XXX Where Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No )-(Select ISNULL(SUM(TSPL_SALE_RETURN_HEAD.Empty_Value ),0) from TSPL_SALE_RETURN_HEAD WHERE TSPL_SALE_RETURN_HEAD.Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AND TSPL_SALE_RETURN_HEAD.Is_Post='Y' AND Convert(Date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<=Convert(Date,'" + Me.dtpAgeof.Value + "',103))) as [Due Amount], Due_Date as [Due Date] , '' AS [type], Sale_Invoice_Date as [Document Date] , DATEDIFF(day,convert(date,TSPL_SALE_INVOICE_HEAD.due_date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days, 'IN' as [Document_Type],(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) As Location from TSPL_SALE_INVOICE_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_SALE_INVOICE_HEAD.Is_Post='Y' " + CheckCustomer + " ) XXX WHERE [Due Amount]>0 " & _
                                "  UNION ALL select TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code ,Parent_Customer_No ,Cust_Name ,Document_No  ,Description , Empty_Value*-1 AS [Due Amount], CONVERT(DATE,Document_Date,103) as [Due Date] ,'', CONVERT(Date,Document_Date,103) as [Document Date] , DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'SR',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location)  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 " + CheckCustomer + " " & _
                                " UNION ALL SELECT TSPL_ADJUSTMENT_HEADER.Comp_Code, TSPL_ADJUSTMENT_HEADER.Customer_CODE,'',TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Adjustment_No,'',case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0)) FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) End AS [Due Amount],convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DueDate,'',convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'AD',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=TSPL_ADJUSTMENT_HEADER.Loc_Code) FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')='' and TSPL_ADJUSTMENT_HEADER.Posted='Y' " + CheckCustomer + " " & _
                                " union All select TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 AND TSPL_VCGL_Head.Is_Empty = 1 " + CheckCustomer + " " & _
                                " UNION ALL select TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name  as AName,TSPL_VCGL_Head.Document_No as DocNo,'', Case When TSPL_VCGL_Head.Is_Empty = 1 Then (CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END) Else 0 End As DueAmt ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' " + CheckCustomer + " " & _
                                " UNION ALL Select TSPL_EXPIRY_HEADER.Comp_Code, Customer_CODE, TSPL_CUSTOMER_MASTER.Parent_Customer_No, TSPL_CUSTOMER_MASTER.Customer_NAME, Document_No, Description, TSPL_JOURNAL_MASTER.Total_Credit_Amt*-1 as DueAmt, Document_Date as DueDate, '' as Type, Document_Date, DATEDIFF(day, TSPL_EXPIRY_HEADER.Document_Date, convert(date,'" + Me.dtpAgeof.Value + "',103)) as AgeingDays, 'EX-ADJ', TSPL_EXPIRY_HEADER.Loc_Code from TSPL_EXPIRY_HEADER LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_EXPIRY_HEADER.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_EXPIRY_HEADER.Customer_CODE WHERE TSPL_EXPIRY_HEADER.Posted='Y' " + CheckCustomer + " "
                strFilledQry = " SELECT  TSPL_Customer_Invoice_Head.Comp_Code, TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," & _
                           " TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name],   TSPL_Customer_Invoice_Head.Document_No  as [Document Id],TSPL_Customer_Invoice_Head.Description as [Desc],case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end as [Due Amount],  TSPL_Customer_Invoice_Head.due_date as [Due Date]," & _
                           " '' AS type, CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date],  DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.due_date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days , case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] ," & _
                           " RIGHT(TSPL_Customer_Invoice_Head.Customer_Control_AC,3) as Location FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_Customer_Invoice_Head.Status='1' " + CheckCustomer + " " & _
                           " UNION ALL Select * from ( SELECT  TSPL_SALE_INVOICE_HEAD.Comp_Code, TSPL_SALE_INVOICE_HEAD.Cust_Code AS [Customer Id], Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name] ,Sale_Invoice_No as [Document Id] ,Description as [Desc] ,Total_Invoice_Amt-(Select ISNULL(SUM(Total_Invoice_Amt),0) from TSPL_SALE_RETURN_HEAD WHERE Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AND TSPL_SALE_RETURN_HEAD.Is_Post='Y' AND Convert(Date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<=Convert(Date,'" + Me.dtpAgeof.Value + "',103)) as [Due Amount], Due_Date as [Due Date] ,'' AS [type], Sale_Invoice_Date as [Document Date] , DATEDIFF(day,convert(date,TSPL_SALE_INVOICE_HEAD.due_date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'IN' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) Location from TSPL_SALE_INVOICE_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_SALE_INVOICE_HEAD.Is_Post='Y' " + CheckCustomer + " ) ZZZ WHere [Due Amount]<>0 " & _
                            "  UNION ALL select TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code ,Parent_Customer_No ,Cust_Name ,Document_No  ,Description ,(Total_Order_Amt)*-1 AS [AMT] , CONVERT(DATE,Document_Date,103) as [Due Date] ,'', CONVERT(Date,Document_Date,103) as [Document Date], DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'SR',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location)  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 " + CheckCustomer + " " & _
                            " union All select TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 AND TSPL_VCGL_Head.Is_Empty <> 1 " + CheckCustomer + " " & _
                            " UNION ALL select TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name  as AName,TSPL_VCGL_Head.Document_No as DocNo,'', Case When TSPL_VCGL_Head.Is_Empty <> 1 Then (CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END) Else 0 End As DueAmt ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' " + CheckCustomer + " " & _
                            " union All select   TSPL_RECEIPT_HEADER.Comp_Code , TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_RECEIPT_HEADER.Customer_Name ,TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_HEADER.Entry_Desc , Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) ,'' AS type ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) , DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" + dtpCutoffDate.Value + "',103)), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as Location from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M')  and TSPL_RECEIPT_HEADER.Posted='Y' AND  Not exists(Select 1 from TSPL_BANK_REVERSE Where Reverse_Document='Receipts' AND Document_No=TSPL_RECEIPT_HEADER.Receipt_No AND Post='P' AND Reversal_Date<=Convert(Date,'" + Me.dtpAgeof.Value + "',103)) " + CheckCustomer + " " & _
                            " UNION ALL Select TSPL_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], Adjustment_No as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], Adjustment_Amount*-1 as [Due Amount], CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" + Me.dtpAgeof.Value + "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SALE_INVOICE_HEAD.Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' " + CheckCustomer + " "
            End If

            If clsCommon.CompairString(cmbType.Text, "Both") = CompairStringResult.Equal Then
                strInnerQry = strFilledQry + " Union All " + strEmptyQry
            ElseIf clsCommon.CompairString(cmbType.Text, "Filled") = CompairStringResult.Equal Then
                strInnerQry = strFilledQry
            ElseIf clsCommon.CompairString(cmbType.Text, "Empty") = CompairStringResult.Equal Then
                strInnerQry = strEmptyQry
            End If

            strUpperQry = " select '" + rptHeading + "' as rptHeading, '" + txtCurr.Text + "' AS First_Period, '" + Me.txt1.Text + "' AS Second_Period, '" + Me.txt2.Text + "' AS [Third Period], '" + Me.txt3.Text + "' AS [Fourth Period], '" + Me.txt4.Text + "' AS [Fifth Period],'" + Me.txt5.Text + "' AS [Sixth Period]," & _
                                " '" + Me.txt6.Text + "' AS [Seventh Period],'" + Me.txt7.Text + "' AS [Eight Period], '" + Me.txt8.Text + "' AS [Nineth Period], '" + txtOvr + "' AS [Over], Query.*, '' AS From_Vendor, '' AS To_Vendor, " & _
                                " '" + Me.ddlAgedRcvbl.Text + "' AS Report_Type,  '" + Me.dtpAgeof.Value + "' AS AgeofDate,'" + strTtpe + "' as [Summary], '" + IsFifoBased + "' as [IsFifoBased], TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc," & _
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
            If chkParentSelect.IsChecked = True Then
                If cbgParentCode.CheckedValue.Count <= 0 Then
                    RadMessageBox.Show("Please Select Atleast One Customer Parent Code.")
                    Return
                End If
                strLowerQry += " and XXX.[Parent Code] in (" + clsCommon.GetMulcallString(cbgParentCode.CheckedValue) + ") "
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
                Dim strFifoQry As String = strUpperQry + strUpperQry1 + strInnerQry + strLowerQry + strLowerQry1
                If ddlAgedRcvbl.Text = "Aged Trial Balance By Document date" Then
                    strFifoQry += " Order By [Document Date]"
                Else
                    strFifoQry += " Order By [Due Date]"
                End If
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(strFifoQry)
                Dim dtFifo As DataTable
                Dim dtFifo1 As DataTable
                Dim dv As DataView
                For Each drCustomer As DataRow In dtCustomer.Rows
                    '--------------------FIFO(-ve balance)-------------------
                    dv = New DataView(dtTemp)
                    dv.RowFilter = "[Customer Id] = '" + clsCommon.myCstr(drCustomer("Customer Id")) + "' AND [Due Amount] < 0"
                    dtFifo = dv.ToTable()
                    '--------------------------------------------------------
                    '--------------------FIFO(+ve balance)-------------------
                    dv = New DataView(dtTemp)
                    dv.RowFilter = "[Customer Id] = '" + clsCommon.myCstr(drCustomer("Customer Id")) + "' AND [Due Amount] > 0"
                    dtFifo1 = dv.ToTable()
                    '--------------------------------------------------------
                    If dtFifo1.Rows.Count <= 0 And dtFifo.Rows.Count > 0 And Not chkCreditBalance.Checked Then
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
                        If NegativeAmt > PositiveAmt And Not chkCreditBalance.Checked Then
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


    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUST-AGE-RPT"
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
    '            'btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub chkParentSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkParentAll.ToggleStateChanged
        cbgParentCode.Enabled = Not chkParentAll.IsChecked
    End Sub

    Private Sub chkCusSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLOcAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLOcAll.IsChecked
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        chkSettlementPending.Checked = False
        chkSettlementPending.Visible = chkActive.Checked
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

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbType.SelectedIndexChanged
        'chkFifo.Enabled = False
        'chkFifo.Checked = False
        'If clsCommon.CompairString(cmbType.Text, "Both") = CompairStringResult.Equal And chkType.Checked Then
        '    chkFifo.Enabled = True
        'End If
    End Sub

    Private Sub chkFifo_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkFifo.ToggleStateChanged
        chkCreditBalance.Checked = False
        If chkFifo.Checked Then
            chkCreditBalance.Enabled = True
        Else
            chkCreditBalance.Enabled = False
        End If
    End Sub

    Private Sub chkSettlementPending_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSettlementPending.ToggleStateChanged
        LoadCustomer()
    End Sub
End Class
