Imports common

Public Class FrmPJVReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.PJVReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub




    Private Sub FrmPJVReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        reset()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")


    End Sub
    Sub reset()

        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chkLocAll.IsChecked = True
        chkVendorAll.IsChecked = True
        rd0pjvall.IsChecked = True

        LoadInvoice()
        LoadLocation()
        LoadVendor()
    End Sub

    Sub LoadInvoice()
        Dim qry As String = " select Invoice_No,Invoice_Date from TSPL_PJV_HEAD "
        cbgpjv.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgpjv.ValueMember = "Invoice_No"
        cbgpjv.DisplayMember = "Invoice_No"
    End Sub

    Sub LoadLocation()
        Dim qry As String = " select Location_Code as Code,Location_Desc as Description from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER  WHERE  Status='N'   order by Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"

    End Sub

    Private Sub rd0pjvall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rd0pjvall.ToggleStateChanged
        cbgpjv.Enabled = Not rd0pjvall.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()

        Dim VendorFilter As String
        Dim LocCodeFilter As String
        Dim DocNoFilter As String
        Dim FromDate As String = dtpFromdate.Value.ToString("dd/MM/yyyy")
        Dim Todate As String = dtptodate.Value.ToString("dd/MM/yyyy")


        Try
            If rdopjvselect.IsChecked AndAlso cbgpjv.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Invoice Number")
                Return

            Else
                DocNoFilter = clsCommon.GetMulcallString(cbgpjv.CheckedValue)
                DocNoFilter = DocNoFilter.Replace("'", "")
            End If
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
                Return
            Else
                VendorFilter = clsCommon.GetMulcallString(cbgVendor.CheckedValue)
                VendorFilter = VendorFilter.Replace("'", "")
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                Return
            Else
                LocCodeFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LocCodeFilter = LocCodeFilter.Replace("'", "")
            End If

            '    Dim qry As String = "  SELECT  TSPL_PJV_HEAD.PJV_No, TSPL_PJV_HEAD.PJV_Date, TSPL_PJV_HEAD.Vendor_Code, TSPL_PJV_HEAD.Vendor_Name, TSPL_PJV_HEAD.PO_No, " & _
            '            " TSPL_PJV_HEAD.PO_Date, TSPL_PJV_HEAD.SRN_No, TSPL_PJV_HEAD.SRN_Date, TSPL_PJV_HEAD.Vendor_Invoice_No, TSPL_PJV_HEAD.Invoice_Date, " & _
            '           "  case when TSPL_PJV_HEAD.Status=0 then 'Pending' else 'Approved'end as status, TSPL_PJV_HEAD.Posting_Date, TSPL_PJV_HEAD.PJV_Amount, TSPL_PJV_HEAD.PJV_TDS_Amount, " & _
            '            " TSPL_PJV_HEAD.PJV_Net_Amount, TSPL_PJV_HEAD.Narration, TSPL_PJV_HEAD.Created_By, TSPL_PJV_Detail.Line_No, " & _
            '             " TSPL_PJV_Detail.GL_Account_Code, TSPL_PJV_Detail.GL_Account_Desc, TSPL_PJV_Detail.PJV_Amount AS Expr1,case when TSPL_PJV_Detail.PJV_Amount<0 then -1 * TSPL_PJV_Detail.PJV_Amount else 0 end as Credit,case when TSPL_PJV_Detail.PJV_Amount>=0 then TSPL_PJV_Detail.PJV_Amount else 0 end as Debit ,TSPL_SRN_HEAD.Bill_To_Location, " & _
            '             " TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, TSPL_COMPANY_MASTER.Add3, " & _
            '           "  TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, " & _
            '            " TSPL_VENDOR_MASTER.Vendor_Name AS Expr2, TSPL_VENDOR_MASTER.Add1 AS Expr3, TSPL_VENDOR_MASTER.Add2 AS Expr4, " & _
            '            " TSPL_VENDOR_MASTER.Add3 AS Expr5, TSPL_VENDOR_MASTER.City_Code_Desc,TSPL_PJV_HEAD .Due_Date " & _
            '" FROM         TSPL_PJV_HEAD LEFT OUTER JOIN " & _
            '           "  TSPL_PJV_Detail ON TSPL_PJV_HEAD.PJV_No = TSPL_PJV_Detail.PJV_No LEFT OUTER JOIN " & _
            '            " TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PJV_HEAD.SRN_No " & _
            '           "   LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PJV_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code LEFT OUTER JOIN " & _
            '            " TSPL_COMPANY_MASTER ON TSPL_PJV_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code where 2=2  and (CONVERT(date, TSPL_PJV_HEAD.Invoice_Date, 103) >= CONVERT(date,'" + dtpFromdate.Value + "', 103)) " & _
            '             "  AND (CONVERT(date, TSPL_PJV_HEAD.Invoice_Date, 103) <= CONVERT(date, '" + dtptodate.Value + " ', 103))   "

            '=============== Added By Abhishek Kumar as On 6 July 2012 ====================
            Dim qry As String = " select '" + FromDate + "' as FromDate,'" + Todate + "'  as Todate,'" + DocNoFilter + "' as DocNoFilter,'" + VendorFilter + "' as VendorNoFilter,'" + LocCodeFilter + "' as LocCodeFilter,  ROW_NUMBER ()over(partition By Pjv_No order by Line_No)as Line_No, *,TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, TSPL_COMPANY_MASTER.Add3,   TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2 from " & _
                                 " (select xx.PJV_No as PJV_No,MAX(xx.PJV_Date)as PJV_Date,MAX(xx.Vendor_Code )as Vendor_Code,MAX(xx.Vendor_Name )as Vendor_Name,MAX(xx.Against_PO )as Against_PO,MAX(xx.Vendor_Invoice_Date)as Vendor_Invoice_Date,MAX(xx.SRN_No)as SRN_No,MAX(xx.SRN_Date)as SRN_Date,MAX(xx.Vendor_Invoice_No )as Vendor_Invoice_No," & _
                                 " max(xx.Invoice_Date )as Invoice_Date,MAX(xx.status )as status ,max(xx.Posting_Date )as Posting_Date,SUM(xx.PJV_Amount )as PJV_Amount,SUM(xx.PJV_TDS_Amount )as PJV_TDS_Amount,SUM(xx.PJV_Net_Amount )as PJV_Net_Amount,max(xx.Narration )as Narration,MAX(xx.Created_By )as Created_By," & _
                                 " xx.GL_Account_Code as GL_Account_Code,max(xx.GL_Account_Desc)as GL_Account_Desc,SUM(xx.Expr1)as Expr1,SUM(xx.Credit ) as Credit,SUM(xx.Debit )as Debit,MAX(xx.Bill_To_Location) as Bill_To_Location,MAX(xx.Expr2)as Expr2,MAX(xx.Expr3)as Expr3,MAX(xx.Expr4 )as Expr4 ,MAX(xx.Expr5 )as Expr5," & _
                                 "  MAX(xx.City_Code_Desc)as City_Code_Desc ,MAX(xx.Due_Date)as Due_Date ,max(xx.Comp_Code )as Comp_Code,MAX(xx.Invoice_No )as Invoice_No  , max(xx.Dept_Desc) as Dept_Desc , max(xx.Line_No ) as Line_No from " & _
                                "  (SELECT  TSPL_PJV_HEAD.PJV_No,convert(varchar, TSPL_PJV_HEAD.PJV_Date,103) as PJV_Date, TSPL_PJV_HEAD.Vendor_Code, TSPL_PJV_HEAD.Vendor_Name, TSPL_PJV_HEAD.PO_No as Against_PO ,  TSPL_PI_HEAD.InvoiceDate as Vendor_Invoice_Date, (select stuff((select DISTINCT ',' + tspl_pi_detail.srn_id from tspl_pi_detail where tspl_pi_detail.PI_No = TSPL_PJV_HEAD.Invoice_No for xml path('')  ),1,1,'')) as SRN_No, TSPL_PJV_HEAD.SRN_Date, TSPL_PJV_HEAD.Vendor_Invoice_No, TSPL_PJV_HEAD.Invoice_Date," & _
                                "    case when TSPL_PI_HEAD.Status=0 then 'Pending' else 'Approved'end as status, TSPL_PJV_HEAD.Posting_Date, TSPL_PJV_HEAD.PJV_Amount, TSPL_PJV_HEAD.PJV_TDS_Amount,  TSPL_PJV_HEAD.PJV_Net_Amount, TSPL_PJV_HEAD.Narration, TSPL_PJV_HEAD.Created_By, TSPL_PJV_Detail.Line_No," & _
                                "   TSPL_PJV_Detail.GL_Account_Code, TSPL_PJV_Detail.GL_Account_Desc, TSPL_PJV_Detail.PJV_Amount AS Expr1,case when TSPL_PJV_Detail.PJV_Amount<0 then -1 * TSPL_PJV_Detail.PJV_Amount else 0 end as Credit,case when TSPL_PJV_Detail.PJV_Amount>=0 then TSPL_PJV_Detail.PJV_Amount else 0 end as Debit ," & _
                                 "TSPL_SRN_HEAD.Bill_To_Location,TSPL_VENDOR_MASTER.Vendor_Name AS Expr2, TSPL_VENDOR_MASTER.Add1 AS Expr3, TSPL_VENDOR_MASTER.Add2 AS Expr4,  TSPL_VENDOR_MASTER.Add3 AS Expr5, TSPL_VENDOR_MASTER.City_Code_Desc,TSPL_PJV_HEAD .Due_Date,TSPL_PJV_HEAD .Comp_Code,TSPL_PJV_HEAD .Invoice_No , TSPL_PJV_HEAD.Dept_Desc   " & _
                                 "  FROM TSPL_PJV_HEAD LEFT OUTER JOIN   TSPL_PJV_Detail ON TSPL_PJV_HEAD.PJV_No = TSPL_PJV_Detail.PJV_No LEFT OUTER JOIN TSPL_PI_HEAD  ON TSPL_PI_HEAD.PI_No = TSPL_PJV_HEAD.Invoice_No LEFT OUTER JOIN  TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PJV_HEAD.SRN_No    LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PJV_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code where 2=2 " & _
                                "  and (CONVERT(date, TSPL_PJV_HEAD.Invoice_Date, 103) >= CONVERT(date,'" + dtpFromdate.Value + "', 103))   AND (CONVERT(date, TSPL_PJV_HEAD.Invoice_Date, 103) <= CONVERT(date,  '" + dtptodate.Value + " ', 103)))"
            qry += " as xx Group By xx.PJV_No , xx.GL_Account_Code)as finalQry LEFT OUTER JOIN  TSPL_COMPANY_MASTER on finalQry .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code where (finalQry .Credit >0 or finalQry .Debit >0)"
            '=================== 
            Dim qry1 As String = "select (select stuff((select DISTINCT ',' + tspl_pi_detail.srn_id from tspl_pi_detail where  TSPL_PI_DETAIL.PI_No=p.pi_no for xml path('')  ),1,1,'')) as SRN_No,P.Item_Code as Item_Code ,isnull(P .PI_Qty,0) as PI_Qty, isnull(P.Short_Qty ,0) as Short_Qty,isnull(P.Reject_Qty ,0) AS Reject_Qty ,P .Item_Desc as Item_Desc,P .Item_GL_Account_Desc as FaAccount,(isnull(P .PI_Qty,0)+ISNULL(P .Burst_Qty ,0)+ISNULL( P .Leak_Qty ,0)+isnull(P.Short_Qty ,0))as  SRN_Qty, P.Landed_Cost_Rate as Landed_Cost_Rate,P.Landed_Cost_Amount as Landed_Cost_Amount ,(case TSPL_SRN_HEAD .Item_Type  when 'F'then'Finished Goods'when 'R' then 'Raw Material' when 'O' then 'Other'when 'P' then 'Promotional Item' else '' end) as Item_Type from TSPL_PI_DETAIL P left outer join TSPL_SRN_HEAD  on TSPL_SRN_HEAD .SRN_No =P .SRN_Id left outer join TSPL_PJV_HEAD on TSPL_PJV_HEAD .SRN_No =TSPL_SRN_HEAD .SRN_No where P .Row_Type <>'Misc'  "


            If rdopjvselect.IsChecked = True Then
                qry += " and finalQry.Invoice_No in(" + clsCommon.GetMulcallString(cbgpjv.CheckedValue) + ")"

                qry1 += " and P.PI_No  in(" + clsCommon.GetMulcallString(cbgpjv.CheckedValue) + ")"
            End If
            'qry1 += " order by TSPL_PI_DETAIL.line_no  "
            If chkLocSelect.IsChecked = True Then
                qry += " and finalQry.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

            End If

            If chkVendorSelect.IsChecked = True Then
                qry += " and finalQry.vendor_code in(" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.myCstr(objCommonVar.CurrentCompanyCode) = "UDL" Then
                frmCRV.funsubreport(CrystalReportFolder.PurchaseOrder, qry, qry1, "rptPJV-V", "PJV Report", "PurchaseDetails1.rpt")
            Else
                frmCRV.funsubreport(CrystalReportFolder.PurchaseOrder, qry, qry1, "rptPJV", "PJV Report", "PurchaseDetails1.rpt")
            End If
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmPJVReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()
        End If
    End Sub
End Class
