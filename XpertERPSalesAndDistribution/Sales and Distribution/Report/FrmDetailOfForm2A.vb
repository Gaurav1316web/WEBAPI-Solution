Imports common
Imports XpertERPEngine

Public Class FrmDetailOfForm2A

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()



    Private Sub FrmDetailOfForm2A_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
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

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub
    Sub print()
        Try
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Vendor")
                Return
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Atleast Single Location Or select All ")
                Return
            End If
            Dim qry As String

            ''   qry = "select Mdate,MD,Vendor_Code,Vendor_Name,Tin_No, TxRate,sum(TxBaseAmt) as TxBaseAmt,sum(TxAmt) as TxAmt, max(comptin) as comptin  from(select * from(select   SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103),106),' ','-' ),4,10)as Mdate, month(convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)) as MD ,  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.Document_No, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as DocNo,convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)as Posting_Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as DocDate,TSPL_VENDOR_MASTER.Tin_No, " & _
            ''       " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX1  )='V'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Rate  " & _
            ''       "   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX2  )='V'   then TSPL_VENDOR_INVOICE_HEAD.TAX2_Rate   " & _
            ''       "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX3  )='V'   then TSPL_VENDOR_INVOICE_HEAD.TAX3_Rate " & _
            ''       "  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX4  )='V'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Rate   " & _
            ''      "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX5  )='V'   then " & _
            ''"   TSPL_VENDOR_INVOICE_HEAD.TAX5_Rate " & _
            '' "       else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX6  )='V'   then " & _
            '' "  TSPL_VENDOR_INVOICE_HEAD.TAX6_Rate" & _
            ''"   else 0  end end end end end end) as TxRate, " & _
            ''  "  (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 1 else -1 end )* ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX1  )='V'  then TSPL_VENDOR_INVOICE_HEAD.Tax1_BAmount   " & _
            '' "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX2  )='V'   then TSPL_VENDOR_INVOICE_HEAD.Tax2_BAmount   " & _
            '' "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX3  )='V'   then TSPL_VENDOR_INVOICE_HEAD.Tax3_BAmount " & _
            '' "  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX4  )='V'  then TSPL_VENDOR_INVOICE_HEAD.Tax4_BAmount   " & _
            '' "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX5  )='V'   then " & _
            '' "  TSPL_VENDOR_INVOICE_HEAD.Tax5_BAmount" & _
            '' "   else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX6  )='V'   then " & _
            '' "  TSPL_VENDOR_INVOICE_HEAD.Tax6_BAmount " & _
            '' "   else 0  end end end end end end) as TxBaseAmt, " & _
            ''" (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 1 else -1 end )* ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX1  )='V'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt  " & _
            '' "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX2  )='V'   then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt   " & _
            ''"  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX3  )='V'   then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt " & _
            ''"  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX4  )='V'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt   " & _
            ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX5  )='V'   then " & _
            '' "  TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt" & _
            ''"  else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_HEAD.TAX6  )='V'   then " & _
            ''"   TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt" & _
            '' "  else 0  end end end end end end) as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1   ,TSPL_COMPANY_MASTER.Tin_No as comptin " & _
            '' "  from  TSPL_VENDOR_INVOICE_HEAD "
            ''   qry += "left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL .Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No ='1'"
            ''   qry += "left outer join TSPL_GL_ACCOUNTS on TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code =TSPL_GL_ACCOUNTS .Account_Code " & _
            ''"  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
            ''"  LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code " & _
            ''"  where 2=2 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "



            qry = "select Mdate,MD,Vendor_Code,Vendor_Name,Tin_No, TxRate,sum(TxBaseAmt) as TxBaseAmt,sum(TxAmt) as TxAmt, max(comptin) as comptin  from(select * from(select   SUBSTRING( REPLACE( convert(varchar(11),convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103),106),' ','-' ),4,10)as Mdate, month(convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)) as MD ,  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.Document_No, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as DocNo,convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)as Posting_Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as DocDate,TSPL_VENDOR_MASTER.Tin_No, " & _
               " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX1  )='V'  then TSPL_VENDOR_INVOICE_DETAIL.TAX1_Rate  " & _
               "   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX2  )='V'   then TSPL_VENDOR_INVOICE_DETAIL.TAX2_Rate   " & _
               "  else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX3  )='V'   then TSPL_VENDOR_INVOICE_DETAIL.TAX3_Rate " & _
               "  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX4  )='V'  then TSPL_VENDOR_INVOICE_DETAIL.TAX4_Rate   " & _
              "  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX5  )='V'   then " & _
        "   TSPL_VENDOR_INVOICE_DETAIL.TAX5_Rate " & _
         "       else case when (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX6  )='V'   then " & _
         "  TSPL_VENDOR_INVOICE_DETAIL.TAX6_Rate" & _
        "   else 0  end end end end end end) as TxRate, " & _
          "(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 1 else -1 end )* ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX1  )='V'  then TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount   else case when  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_VENDOR_INVOICE_DETAIL.TAX3  )='V'   then (TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount+TSPL_VENDOR_INVOICE_DETAIL.TAX1_Amt+TSPL_VENDOR_INVOICE_DETAIL.TAX2_Amt)   " & _
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
         "  else 0  end end end end end end) as TxAmt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1   ,TSPL_COMPANY_MASTER.Tin_No as comptin " & _
         "  from  TSPL_VENDOR_INVOICE_HEAD "
            qry += "left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD .Document_No =TSPL_VENDOR_INVOICE_DETAIL .Document_No "
            qry += "left outer join TSPL_GL_ACCOUNTS on TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code =TSPL_GL_ACCOUNTS .Account_Code " & _
         "  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
         "  LEFT Outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code " & _
         "  where 2=2 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')<>'' "




            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            qry += "  and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)>='" + clsCommon.GetPrintDate(fromdate.Value, "yyyy-MM-dd") + "' and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)<='" + clsCommon.GetPrintDate(Todate.Value, "yyyy-MM-dd") + "' and TSPL_VENDOR_INVOICE_HEAD.Posting_Date  is not null  )final where TxRate<>0  "



            If chkVendorSelect.IsChecked Then
                qry += " and Vendor_Code in  (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
            End If
            qry += " ) main  group by  Mdate,MD,Vendor_Code,Vendor_Name,Tin_No,TxRate order by MD"



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(qry), "DetailOfForm2A", "Detail Of Form2A")
            End If






        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub


End Class
