Imports common

Public Class FrmDebitAdviseReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.DebitAdviseReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 
    Private Sub LoadLocations()
        Try
            Dim qry As String = "select Location_Code Code , Location_Desc Name from TSPL_LOCATION_MASTER"
            cbgLocations.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgLocations.DisplayMember = "Name"
            cbgLocations.ValueMember = "Code"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FrmDebitAdviseReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            reset()
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
            ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")

            'If objCommonVar.CurrentUserCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "DEBIT-RPT"
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()
        Dim SrnNo As String
        Dim Vendor As String
       
        Dim Fromdate As String
        Dim Todate As String
        Try
            If chksrnselect.IsChecked AndAlso cbgSRN.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one SRN No.", Me.Text)
                Return
            Else
                SrnNo = clsCommon.GetMulcallString(cbgSRN.CheckedValue)
                SrnNo = SrnNo.Replace("'", "")
            End If
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Vendor", Me.Text)
                Return
            Else
                Vendor = clsCommon.GetMulcallString(cbgVendor.CheckedValue)
                Vendor = Vendor.Replace("'", "")

            End If
            Fromdate = dtpFromdate.Value.ToString("dd-MM-yyyy")
            Todate = dtptodate.Value.ToString("dd-MM-yyyy")
            Dim qry As String


            '' qry = " SELECT  ('We have debited your A/c by  Rs ' +convert(varchar(20),TSPL_PR_HEAD.PR_Total_Amt)+ '  on A/c of QC Rejection of material supplied by you vide billNo.  '+TSPL_PR_HEAD.Vendor_Invoice_No+'  dt  '+ case when  CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103) IS NULL then'' else  CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103)  end +'  and received through  GIN No.  '+TSPL_PR_HEAD.Against_SRN+'  dt  '+CONVERT(varchar(12), TSPL_SRN_HEAD.SRN_Date, 103)+' . Material were rejected as per following details. ')as detail, " & _
            ''       " TSPL_PR_HEAD.PR_No, TSPL_PR_HEAD.PR_Date, TSPL_PR_HEAD.Vendor_Code, TSPL_PR_HEAD.Vendor_Name,  " & _
            ''          "  TSPL_PR_HEAD.Vendor_Invoice_No, CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103) AS InDate, TSPL_PR_HEAD.Against_SRN, " & _
            ''          "  CONVERT(varchar(12), TSPL_SRN_HEAD.SRN_Date, 103) AS SRNDate, TSPL_PR_HEAD.Against_SRN AS srn, TSPL_PR_HEAD.PR_Total_Amt, " & _
            ''          "  TSPL_PR_DETAIL.Item_Code, TSPL_PR_DETAIL.Item_Desc, TSPL_PR_DETAIL.Unit_code, TSPL_PR_DETAIL.PR_Qty, TSPL_PR_DETAIL.Item_Cost, " & _
            ''          "  TSPL_PR_DETAIL.Amount, TSPL_PR_DETAIL.Amt_Less_Discount, TSPL_PR_DETAIL.Total_Tax_Amt, TSPL_PR_DETAIL.Item_Net_Amt, " & _
            ''          "  TSPL_VENDOR_MASTER.Add1, TSPL_VENDOR_MASTER.Add2, TSPL_VENDOR_MASTER.Add3, TSPL_COMPANY_MASTER.Comp_Name, " & _
            ''           " TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Add1 AS ComAdd, TSPL_PR_HEAD.TAX1, TSPL_PR_HEAD.TAX1_Amt, " & _
            ''           " TSPL_PR_HEAD.TAX2, TSPL_PR_HEAD.TAX2_Amt, TSPL_PR_HEAD.TAX3, TSPL_PR_HEAD.TAX3_Amt, TSPL_PR_HEAD.TAX4, " & _
            ''           " TSPL_PR_HEAD.TAX4_Amt, TSPL_PR_HEAD.TAX5, TSPL_PR_HEAD.TAX5_Amt, TSPL_PR_HEAD.TAX6, TSPL_PR_HEAD.TAX6_Amt, " & _
            ''           " TSPL_PR_HEAD.TAX7, TSPL_PR_HEAD.TAX7_Amt, TSPL_PR_HEAD.TAX8, TSPL_PR_HEAD.TAX8_Amt, TSPL_PR_HEAD.TAX9, " & _
            ''"  TSPL_PR_HEAD.TAX9_Amt, TSPL_PR_HEAD.TAX10, TSPL_PR_HEAD.TAX10_Amt " & _
            ''  "     FROM         TSPL_PR_HEAD LEFT OUTER JOIN " & _
            ''          "  TSPL_PR_DETAIL ON TSPL_PR_HEAD.PR_No = TSPL_PR_DETAIL.PR_No LEFT OUTER JOIN " & _
            ''          "  TSPL_PI_HEAD ON TSPL_PI_HEAD.Vendor_Invoice_No = TSPL_PR_HEAD.Vendor_Invoice_No LEFT OUTER JOIN " & _
            ''          "  TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = TSPL_PR_HEAD.Against_SRN LEFT OUTER JOIN " & _
            ''          "  TSPL_COMPANY_MASTER ON TSPL_PR_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
            ''           " TSPL_VENDOR_MASTER ON TSPL_PR_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code where 2=2 and   (CONVERT(date, TSPL_PR_HEAD.PR_Date, 103) >= CONVERT(date,'" + dtpFromdate.Value + "', 103)) " & _
            ''          "  AND (CONVERT(date, TSPL_PR_HEAD.PR_Date, 103) <= CONVERT(date, '" + dtptodate.Value + " ', 103)) "



            qry = " SELECT '" + Fromdate + "' as FromDate,'" + Todate + "' as Todate, '" + Vendor + "' as VendorFilter,'" + SrnNo + "' as SrnFilter,  case when TSPL_PR_HEAD.Against_SRN='' OR TSPL_PR_HEAD.Against_SRN Is null then 'We have debited your A/c by  Rs ' +convert(varchar(20),TSPL_PR_HEAD.PR_Total_Amt)+ '  on A/c of QC Rejection of material supplied by you  . Material were rejected as per following details. ' else ('We have debited your A/c by  Rs ' +convert(varchar(20),TSPL_PR_HEAD.PR_Total_Amt)+ '  on A/c of QC Rejection of material supplied by you vide billNo.  '+TSPL_PR_HEAD.Vendor_Invoice_No+'  dt  '+ case when  CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103) IS NULL then'' else  CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103)  end +'  and received through  GIN No.  '+TSPL_PR_HEAD.Against_SRN+'  dt  '+CONVERT(varchar(12), TSPL_SRN_HEAD.SRN_Date, 103)+' . Material were rejected as per following details. ') end as detail, " & _
                 " TSPL_PR_HEAD.PR_No, TSPL_PR_HEAD.PR_Date, TSPL_PR_HEAD.Vendor_Code, TSPL_PR_HEAD.Vendor_Name,  " & _
                    "  TSPL_PR_HEAD.Vendor_Invoice_No, TSPL_PR_HEAD.Bill_To_Location as [Location Code], (select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =  TSPL_PR_HEAD.Bill_To_Location) AS [Location Desc],  CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103) AS InDate, TSPL_PR_HEAD.Against_SRN, " & _
                    "  CONVERT(varchar(12), TSPL_SRN_HEAD.SRN_Date, 103) AS SRNDate, TSPL_PR_HEAD.Against_SRN AS srn, TSPL_PR_HEAD.PR_Total_Amt, " & _
                    "  TSPL_PR_DETAIL.Item_Code, TSPL_PR_DETAIL.Item_Desc, TSPL_PR_DETAIL.Unit_code, TSPL_PR_DETAIL.PR_Qty, TSPL_PR_DETAIL.Item_Cost, " & _
                    "  TSPL_PR_DETAIL.Amount, TSPL_PR_DETAIL.Amt_Less_Discount, TSPL_PR_DETAIL.Total_Tax_Amt, TSPL_PR_DETAIL.Item_Net_Amt, " & _
                    "  TSPL_VENDOR_MASTER.Add1, TSPL_VENDOR_MASTER.Add2, TSPL_VENDOR_MASTER.Add3, TSPL_COMPANY_MASTER.Comp_Name, " & _
                     " TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Add1 AS ComAdd, TSPL_PR_HEAD.TAX1, TSPL_PR_HEAD.TAX1_Amt, " & _
                     " TSPL_PR_HEAD.TAX2, TSPL_PR_HEAD.TAX2_Amt, TSPL_PR_HEAD.TAX3, TSPL_PR_HEAD.TAX3_Amt, TSPL_PR_HEAD.TAX4, " & _
                     " TSPL_PR_HEAD.TAX4_Amt, TSPL_PR_HEAD.TAX5, TSPL_PR_HEAD.TAX5_Amt, TSPL_PR_HEAD.TAX6, TSPL_PR_HEAD.TAX6_Amt, " & _
                     " TSPL_PR_HEAD.TAX7, TSPL_PR_HEAD.TAX7_Amt, TSPL_PR_HEAD.TAX8, TSPL_PR_HEAD.TAX8_Amt, TSPL_PR_HEAD.TAX9, " & _
          "  TSPL_PR_HEAD.TAX9_Amt, TSPL_PR_HEAD.TAX10, TSPL_PR_HEAD.TAX10_Amt " & _
            "     FROM         TSPL_PR_HEAD LEFT OUTER JOIN " & _
                    "  TSPL_PR_DETAIL ON TSPL_PR_HEAD.PR_No = TSPL_PR_DETAIL.PR_No LEFT OUTER JOIN " & _
                    "  TSPL_PI_HEAD ON TSPL_PI_HEAD.Vendor_Invoice_No = TSPL_PR_HEAD.Vendor_Invoice_No LEFT OUTER JOIN " & _
                    "  TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = TSPL_PR_HEAD.Against_SRN LEFT OUTER JOIN " & _
                    "  TSPL_COMPANY_MASTER ON TSPL_PR_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                     " TSPL_VENDOR_MASTER ON TSPL_PR_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code where 2=2 and   (CONVERT(date, TSPL_PR_HEAD.PR_Date, 103) >= CONVERT(date,'" + dtpFromdate.Value + "', 103)) " & _
                    "  AND (CONVERT(date, TSPL_PR_HEAD.PR_Date, 103) <= CONVERT(date, '" + dtptodate.Value + " ', 103)) "



            If chkVendorSelect.IsChecked = True Then
                qry += "and TSPL_PR_HEAD.Vendor_Code in(" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
            End If

            If chksrnselect.IsChecked = True Then
                qry += "and  TSPL_PR_HEAD.PR_No  in(" + clsCommon.GetMulcallString(cbgSRN.CheckedValue) + ")"
            End If
            'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 > ADDED LOCATION FILTER
            If rbLocationsSelect.IsChecked And cbgLocations.CheckedValue.Count > 0 Then
                qry += " AND TSPL_PR_HEAD.Bill_To_Location IN (" + clsCommon.GetMulcallString(cbgLocations.CheckedValue) + ")"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptDebitAdivseReport", "Debit Advise Report")
                frmCRV = Nothing
            End If





        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Sub reset()
        Try
            dtpFromdate.Value = clsCommon.GETSERVERDATE()
            dtptodate.Value = clsCommon.GETSERVERDATE()
            LoadVendor()
            LoadSRN()
            LoadLocations()
            chksrnall.IsChecked = True
            chkVendorAll.IsChecked = True
            'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER
            cbgLocations.UnCheckedAll()
            rbLocationsAll.IsChecked = True
        Catch ex As Exception

        End Try
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER  WHERE  Status='N'   order by Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"

    End Sub

    Sub LoadSRN()
        'Dim qry As String = " select TSPL_PR_HEAD.Against_SRN as SRN ,convert(varchar(12),TSPL_SRN_HEAD.SRN_Date,103) as SRNdate from TSPL_PR_HEAD left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PR_HEAD.Against_SRN "
        'cbgSRN.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgSRN.ValueMember = "SRN"
        'cbgSRN.DisplayMember = "SRN"


        Dim qry As String = " select TSPL_PR_HEAD.PR_No as PRN ,convert(varchar(12),TSPL_PR_HEAD.PR_Date,103) as PRNdate from TSPL_PR_HEAD "
        cbgSRN.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSRN.ValueMember = "PRN"
        cbgSRN.DisplayMember = "PRN"
    End Sub

    Private Sub chksrnall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chksrnall.ToggleStateChanged
        cbgSRN.Enabled = Not chksrnall.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub FrmDebitAdviseReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()
        End If

    End Sub
    'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER
    Private Sub rbLocationsAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbLocationsAll.ToggleStateChanged, rbLocationsSelect.ToggleStateChanged
        Try
            cbgLocations.Enabled = rbLocationsSelect.IsChecked
        Catch ex As Exception

        End Try
    End Sub

End Class
