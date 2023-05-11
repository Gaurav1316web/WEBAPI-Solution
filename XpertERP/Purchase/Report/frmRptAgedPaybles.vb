''Replaced by pankaj's report
Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
'-17/09/2013--Version-2.0.2.35----Updation Pankaj Kumar [BM00000000383, BM00000000303, BM00000000465]
'---Updation By [Pankaj Kumar Chaudhary] Against Ticket No-- [BM00000000566,BM00000000772, BM00000000566, BM00000000835]
Public Class FrmRptAgedPaybles
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox1.Click

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAgingPayble)
        'If Not (MyBase.isReadFlag) Then
        '    RadMessageBox.Show("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btnPrint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmRptAgedPaybles_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadlocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        SetUserMgmtNew()
        dtpAgeof.Value = Date.Today
        dtpCutoffDate.Value = Date.Today
        LoadVendor()
        chkInvoice.Checked = True
        chkCreditNote.Checked = True
        chkDebitNote.Checked = True
        chkPayment.Visible = True
        chkAdvance.Checked = True
        chkPayment.Checked = True
        chkOnAccount.Checked = True

        chkVendorAll.IsChecked = True
        chkCreditBalance.Visible = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        txt6th.Text = ""
        txt6th.Enabled = False
        txt7th.Text = ""
        txt7th.Enabled = False
        txt8th.Text = ""
        txt8th.Enabled = False
        txtOver.Text = ""
        txtOver.Enabled = False
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER order by Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        'cbgVendor.DisplayMember = "Vendor_Name"
    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'PrintNew()
        PrintNew1()
    End Sub


    Sub PrintNew1()
        Try
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please Select atleast one Location.")
                Return
            End If
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please Select atleast one Vendor.")
                Return
            End If
            Dim txtOvr As String
            Dim strNo As String
            Dim type As String = Me.ddlAgedPayble.Text
            Dim strType As String = ""
            Dim IsFifoBased As String = "N"

            Dim RunDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy")
            Dim AgeOfDate As String = clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy")
            Dim asofdate As String = clsCommon.GetPrintDate(dtpAgeof.Value, "yyyy-MM-dd")
            Dim cutoffdate As String = clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy")

            If chkType.Checked = True Then
                strType = "SMry"
            End If
            If chkFifo.Checked Then
                IsFifoBased = "Y"
            End If
            '------------------------------------------------------------
            Dim Arr As New ArrayList
            If chkInvoice.Checked = True Then
                Arr.Add("I")
            End If
            If chkCreditNote.Checked = True Then
                Arr.Add("C")
            End If
            If chkDebitNote.Checked = True Then
                Arr.Add("D")
            End If
            If chkAdvance.Checked = True Then
                Arr.Add("AV")
            End If
            If chkOnAccount.Checked = True Then
                Arr.Add("OA")
            End If
            If chkPayment.Checked = True Then
                Arr.Add("P")
            End If
            Arr.Add("RC")
            If Arr.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Transaction Type")
                Return
            End If
            Dim Str1ForDateDiff As String
            If ddlAgedPayble.Text = "Aged Payble by Due Date" Then
                Str1ForDateDiff = "Due_Date"
            Else
                Str1ForDateDiff = "Invoice_Entry_Date"
            End If
            If Me.txtIst.Text = "" Or Me.txt2nd.Text = "" Or Me.txt3rd.Text = "" Then
                RadMessageBox.Show("Select Atleast 3 Buckets!")
                Exit Sub
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text = "" And Me.txt5th.Text = "" And Me.txt6th.Text = "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
                strNo = "0"
                txtOvr = Me.txt3rd.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text = "" And Me.txt6th.Text = "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
                strNo = "1"
                txtOvr = Me.txt4th.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text = "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
                strNo = "2"
                txtOvr = Me.txt5th.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text <> "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
                strNo = "3"
                txtOvr = Me.txt6th.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text <> "" And Me.txt7th.Text <> "" And Me.txt8th.Text = "" Then
                strNo = "4"
                txtOvr = Me.txt7th.Text
            ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text <> "" And Me.txt7th.Text <> "" And Me.txt8th.Text <> "" Then
                strNo = ""
                txtOvr = Me.txtOver.Text
            Else
                RadMessageBox.Show("Selection Criteria Not In Order")
                Exit Sub
            End If

            Dim IsVendorGroupWise As String
            If chkVendorGroupWise.Checked Then
                IsVendorGroupWise = " 1 as IsVendorGroupWise "
            Else
                IsVendorGroupWise = " 0 as IsVendorGroupWise"
            End If
            Dim Qry As String
            Dim dt As DataTable

            Dim strUpperQry As String = "select '" + RunDate + "' as RunDate, '" + AgeOfDate + "' as AgeOfDate, '" + cutoffdate + "' as CutOfDate, '' as rptHeading, '" + Me.txtCurrent.Text + "' AS First_Period, '" + Me.txtIst.Text + "' AS Second_Period, '" + Me.txt2nd.Text + "' AS [Third Period], '" + Me.txt3rd.Text + "' AS [Fourth Period], '" + Me.txt4th.Text + "' AS [Fifth Period],"
            strUpperQry += " '" + Me.txt5th.Text + "' AS [Sixth Period], '" + Me.txt6th.Text + "' AS [Seventh Period],'" + Me.txt7th.Text + "' AS [Eight Period], '" + Me.txt8th.Text + "' AS [Nineth Period], '" + txtOvr + "' AS [Over], "
            strUpperQry += " TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc as Cust_Group_Desc,  xxx.Vendor_Code as [Customer Id], '' as [Parent Code], TSPL_VENDOR_MASTER.Vendor_Name AS [Customer Name], "
            strUpperQry += " xxx.DocNo as [Document Id], '' as [Desc], Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 Else [Document_Total] End as [Due Amount], Due_Date as [Due Date], xxx.DocDate as [Document Date],  datedifference as [Ageing_Days], "
            strUpperQry += " case when Document_Type='I' then 'IN' when Document_Type='PY' then 'PY' when Document_Type='D' then 'DR' when Document_Type='C' then 'CR' when Document_Type='AV' then 'AV' when Document_Type='OA' then 'OA'when Document_Type='PY' then 'PY' when Document_Type='RC' then 'RC' end as [Document_Type], "
            strUpperQry += " '' AS From_Vendor, '' AS To_Vendor,  '" + ddlAgedPayble.Text + "' AS Report_Type,  '" + AgeOfDate + "' AS AgeofDate,'" + strType + "' as [Summary], 'N' as [IsFifoBased], "
            strUpperQry += " TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Add1+case  when isnull(TSPL_COMPANY_MASTER.Add2,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add2 +case  when isnull(TSPL_COMPANY_MASTER.Add3,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 end end as comp_address"
            strUpperQry += " FROM ( "

            Dim strInnerQry As String = " select Vendor_code,Vendor_Name ,Document_No as DocNo , Document_Type , COnvert(Date,Invoice_Entry_Date, 103) as DocDate ,Convert(Date,Posting_Date, 103) as Posting_Date, Convert(Date,Due_Date, 103) as Due_Date , (Case When TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 Then TSPL_VENDOR_INVOICE_HEAD.Document_Total Else TSPL_VENDOR_INVOICE_HEAD.Document_Total-TDS_Actual_Amount-ISNULL((Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD as VH Where VH.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AND VH.Vendor_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No AND VH.Document_Type IN ('D','C') AND ISNULL(VH.Against_PurchaseReturn_No,'')<>''),0) End)-ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No Where TSPL_PAYMENT_HEADER.Posted=1 AND TSPL_PAYMENT_HEADER.IsChkReverse<>'Y' AND TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No AND  CONVERT(DATE,TSPL_PAYMENT_HEADER.Payment_Date,103)<=CONVERT(DATE,'" + cutoffdate + "',103)),0) as [Document_Total], DATEDIFF(dd,convert(date," + Str1ForDateDiff + ",103),'" + asofdate + "') as datedifference, 0 as Payment_Amount , Vendor_Invoice_No  as invno, TSPL_VENDOR_INVOICE_HEAD.Comp_Code, TSPL_VENDOR_INVOICE_HEAD.Loc_Code AS Location  from TSPL_VENDOR_INVOICE_HEAD "
            strInnerQry += " where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<> '' And (Len(ISNULL(Against_POInvoice_No, ''))<=0 AND Len(ISNULL(Against_PurchaseReturn_No, ''))<=0) or Document_Type='I'  and TSPL_VENDOR_INVOICE_HEAD.Posting_Date <> ''   "
            strInnerQry += " UNION ALL "
            strInnerQry += " select  Vendor_code,Vendor_Name ,TSPL_PAYMENT_HEADER.Payment_No as DocNo ,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type , Convert(Date,Payment_Date, 103) as DocDate , Convert(Date,Payment_Post_Date, 103) as Posting_Date, Convert(Date,Payment_Date, 103) as Due_Date ,  Payment_Amount+TDS_Amount-isnull((case when TSPL_PAYMENT_HEADER.Payment_Type in('AV','OA','RC') then (select Amount from TSPL_BANK_REVERSE where Source_Type='AP' and Document_No=TSPL_PAYMENT_HEADER.Payment_No AND TSPL_BANK_REVERSE.Post<>'N' AND TSPL_BANK_REVERSE.Reversal_Date<=Convert(Date,'" + cutoffdate + "', 103)) else 0 end ),0) as [Document_Total]"
            strInnerQry += " ,DATEDIFF(dd,convert(date,Payment_Date,103), '" + asofdate + "') as datedifference, Case When TSPL_PAYMENT_HEADER.Payment_Type='RC' Then Payment_Amount*-1 Else Payment_Amount End as Payment_Amount,''  as invno, TSPL_PAYMENT_HEADER.Comp_Code, RIGHT(TSPL_BANK_MASTER.BANKACC , 3) as Location  from TSPL_PAYMENT_HEADER "
            strInnerQry += " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code= TSPL_BANK_MASTER.BANK_CODE where TSPL_PAYMENT_HEADER.Payment_Type NOT IN ('PY','MI') And (Posted='P' or Posted='1')  "
            strInnerQry += " UNION ALL "
            strInnerQry += " select  VC_Code as Vendor_code,VC_Name as Vendor_Name, Document_No as DocNo, case when Amount_Type='Cr' then 'D' else  'C' end as Document_Type,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, "
            strInnerQry += " CONVERT(Date,TSPL_VCGL_Head.Posting_Date ,103) as Posting_Date, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as Due_Date, Amount as [Document_Total], DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103) , '" + asofdate + "') as datedifference, amount as Payment_Amount,'' as invno, TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location  from TSPL_VCGL_Head  where Document_Type='v' and TSPL_VCGL_Head.Status='1'  "
            strInnerQry += " UNION ALL "
            strInnerQry += " select  TSPL_VCGL_Detail.VCGL_Code  as Vendor_code, TSPL_VCGL_Detail.VCGL_Name  as Vendor_Name, TSPL_VCGL_Detail.Document_No as DocNo, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then 'C' when TSPL_VCGL_Detail.Cr_Amount=0.0 then 'D' end as Document_Type, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, CONVERT(Date,TSPL_VCGL_Head.Posting_Date ,103) as Posting_Date, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as Due_Date, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then TSPL_VCGL_Detail.Cr_Amount when TSPL_VCGL_Detail.Cr_Amount=0.0 then TSPL_VCGL_Detail.Dr_Amount end as [Document_Total] ,DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103), '" + asofdate + "') as datedifference, amount as Payment_Amount,'' as invno , TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location from  TSPL_VCGL_Detail "
            strInnerQry += " left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No   "
            strInnerQry += " where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1'  "



            Dim strLowerQry As String = " ) xxx Left Outer Join TSPL_COMPANY_MASTER ON xxx.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN TSPL_VENDOR_MASTER ON xxx.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code where Document_Total<>0 "
            strLowerQry += " and Convert(Date, DocDate, 103) <=Convert(Date,'" + cutoffdate + "', 103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 1 Then
                strLowerQry += " AND Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count >= 1 Then
                strLowerQry += " AND xxx.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
            End If
            'Dim dtTemp As DataTable
            If chkFifo.Checked Then
                dt = clsDBFuncationality.GetDataTable(strUpperQry + strInnerQry + strLowerQry + "AND 1=2")
                Dim dtVendor As DataTable = clsDBFuncationality.GetDataTable("Select Distinct xxx.Vendor_Code from ( " + strInnerQry + strLowerQry + "")
                For Each drVendor As DataRow In dtVendor.Rows
                    '--------------------FIFO(-ve balance)-------------------
                    Dim strFifoQry As String = strUpperQry + strInnerQry + strLowerQry
                    strFifoQry += " and Document_Type  in ('D','AV','OA')"
                    strFifoQry += " and xxx.Vendor_Code = '" + clsCommon.myCstr(drVendor("Vendor_Code")) + "'"
                    Dim dtFifo As DataTable = clsDBFuncationality.GetDataTable(strFifoQry)
                    '--------------------------------------------------------
                    '--------------------FIFO(+ve balance)-------------------
                    Dim strFifoQry1 As String = strUpperQry + strInnerQry + strLowerQry
                    strFifoQry1 += " and xxx.Vendor_Code = '" + clsCommon.myCstr(drVendor("Vendor_Code")) + "'"
                    strFifoQry1 += " and Document_Type NOT in ('D','AV','OA')"
                    Dim dtFifo1 As DataTable = clsDBFuncationality.GetDataTable(strFifoQry1)
                    '--------------------------------------------------------
                    If dtFifo1.Rows.Count <= 0 And dtFifo.Rows.Count > 0 And Not chkCreditBalance.Checked Then  '--Insert only -Ve Balance
                        For Each dr As DataRow In dtFifo.Rows
                            Dim dRow As DataRow = dt.NewRow()
                            dRow.ItemArray = dr.ItemArray
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    If dtFifo1.Rows.Count > 0 And dtFifo.Rows.Count <= 0 Then                                   '--Insert only +Ve Balance
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
                strLowerQry += " and Document_Type  in (" + clsCommon.GetMulcallString(Arr) + " )  "
                If Not chkType.Checked Then
                    strLowerQry += " Order by xxx.Vendor_Code, xxx.DocDate"
                End If
                Qry = strUpperQry + strInnerQry + strLowerQry
                dt = clsDBFuncationality.GetDataTable(Qry)
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.Purchase, dt, "crptAPAge" + strNo + "", "A/P Aged Paybles Report")
            frmCRV = Nothing
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try

    End Sub




    Sub PrintNew()
        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            RadMessageBox.Show("Please Select atleast one Location.")
            Return
        End If
        If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
            RadMessageBox.Show("Please Select atleast one Vendor.")
            Return
        End If
        '----------Added By-Pankaj Kumar -------------on---02/05/2012-----------For Print on Report----
        Dim RunDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy")
        Dim AgeOfDate As String = clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy")
        '-----------------------------------Code Ends Here---------------------------------------------
        Dim asofdate As String = clsCommon.GetPrintDate(dtpAgeof.Value, "yyyy-MM-dd")
        Dim cutoffdate As String = clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy")

        Dim lblcurrent As String = clsCommon.myCstr(clsCommon.myCdbl(txtCurrent.Text))
        Dim lbl_1 As String = "" + clsCommon.myCstr(clsCommon.myCdbl(txtCurrent.Text) + 1) + " To " + clsCommon.myCstr(txtIst.Text) + ""
        Dim lbl_2 As String = "" + clsCommon.myCstr(clsCommon.myCdbl(txtIst.Text) + 1) + " To " + clsCommon.myCstr(txt2nd.Text) + ""
        Dim lbl_3 As String = "" + clsCommon.myCstr(clsCommon.myCdbl(txt2nd.Text) + 1) + " To " + clsCommon.myCstr(txt3rd.Text) + ""
        Dim lbl_4 As String = "" + clsCommon.myCstr(clsCommon.myCdbl(txt3rd.Text) + 1) + " To " + clsCommon.myCstr(txt4th.Text) + ""
        Dim lbl_5 As String = "" + clsCommon.myCstr(clsCommon.myCdbl(txt4th.Text) + 1) + " To " + clsCommon.myCstr(txt5th.Text) + ""
        Dim lbl_6 As String = "" + clsCommon.myCstr(clsCommon.myCdbl(txt5th.Text) + 1) + " To " + clsCommon.myCstr(txt6th.Text) + ""
        Dim lbl_7 As String = "" + clsCommon.myCstr(clsCommon.myCdbl(txt6th.Text) + 1) + " To " + clsCommon.myCstr(txt7th.Text) + ""
        Dim lbl_8 As String = "" + clsCommon.myCstr(clsCommon.myCdbl(txt7th.Text) + 1) + " To " + clsCommon.myCstr(txt8th.Text) + ""
        Dim lbl_Over120 As String = "Over " + txtOver.Text + ""
        Dim strDtDCurrent As String = "datedifference<= " + txtCurrent.Text + ""
        Dim strDtD_1 As String = "datedifference <= " + clsCommon.myCstr(txtIst.Text) + ""
        Dim strDtD_2 As String = "datedifference >=" + clsCommon.myCstr(clsCommon.myCdbl(txtIst.Text) + 1) + " and datedifference <=" + clsCommon.myCstr(txt2nd.Text) + " "
        Dim strDtD_3 As String = "datedifference >=" + clsCommon.myCstr(clsCommon.myCdbl(txt2nd.Text) + 1) + " and datedifference <=" + clsCommon.myCstr(txt3rd.Text) + " "
        Dim strDtD_4 As String = "datedifference >=" + clsCommon.myCstr(clsCommon.myCdbl(txt3rd.Text) + 1) + " and datedifference <=" + clsCommon.myCstr(txt4th.Text) + " "
        Dim strDtD_5 As String = "datedifference >=" + clsCommon.myCstr(clsCommon.myCdbl(txt4th.Text) + 1) + " and datedifference <=" + clsCommon.myCstr(txt5th.Text) + " "
        Dim strDtD_6 As String = "datedifference >=" + clsCommon.myCstr(clsCommon.myCdbl(txt5th.Text) + 1) + " and datedifference <=" + clsCommon.myCstr(txt6th.Text) + " "
        Dim strDtD_7 As String = "datedifference >=" + clsCommon.myCstr(clsCommon.myCdbl(txt6th.Text) + 1) + " and datedifference <=" + clsCommon.myCstr(txt7th.Text) + " "
        Dim strDtD_8 As String = "datedifference >=" + clsCommon.myCstr(clsCommon.myCdbl(txt7th.Text) + 1) + " and datedifference <=" + clsCommon.myCstr(txt8th.Text) + " "
        Dim strDtD_Over As String = "datedifference >" + clsCommon.myCstr(txt8th.Text) + ""
        If clsCommon.myLen(txt2nd.Text) <= 0 Then
            strDtD_Over = "datedifference >" + clsCommon.myCstr(txtIst.Text) + ""
        ElseIf clsCommon.myLen(txt3rd.Text) <= 0 Then
            strDtD_Over = "datedifference >" + clsCommon.myCstr(txt2nd.Text) + ""
        ElseIf clsCommon.myLen(txt4th.Text) <= 0 Then
            strDtD_Over = "datedifference >" + clsCommon.myCstr(txt3rd.Text) + ""
        ElseIf clsCommon.myLen(txt5th.Text) <= 0 Then
            strDtD_Over = "datedifference >" + clsCommon.myCstr(txt4th.Text) + ""
        ElseIf clsCommon.myLen(txt6th.Text) <= 0 Then
            strDtD_Over = "datedifference >" + clsCommon.myCstr(txt5th.Text) + ""
        ElseIf clsCommon.myLen(txt7th.Text) <= 0 Then
            strDtD_Over = "datedifference >" + clsCommon.myCstr(txt6th.Text) + ""
        ElseIf clsCommon.myLen(txt8th.Text) <= 0 Then
            strDtD_Over = "datedifference >" + clsCommon.myCstr(txt7th.Text) + ""
        End If


        '---This String Is Used When Type Is Changed as [Aged Payble by Due Date] OR [Aged Payble by Document Date] And Is Used In Only Data From Vendor Invoice Head----
        Dim Str1ForDateDiff As String
        If ddlAgedPayble.Text = "Aged Payble by Due Date" Then
            Str1ForDateDiff = "Due_Date"
        Else
            Str1ForDateDiff = "Invoice_Entry_Date"
        End If
        '----------------------------------------------------------------------------------------------------------------------------------------------------------------
        '------------------------

        Dim Arr As New ArrayList
        If chkInvoice.Checked = True Then
            Arr.Add("I")
        End If
        If chkCreditNote.Checked = True Then
            Arr.Add("C")
        End If
        If chkDebitNote.Checked = True Then
            Arr.Add("D")
        End If
        If chkAdvance.Checked = True Then
            Arr.Add("AV")
        End If
        If chkOnAccount.Checked = True Then
            Arr.Add("OA")
        End If
        If chkPayment.Checked = True Then
            Arr.Add("P")
        End If
        Arr.Add("RC")
        If Arr.Count <= 0 Then
            RadMessageBox.Show("Please select at least one Transaction Type")
            Return
        End If

        Dim txtOvr As String
        Dim strNo As String

        If Me.txtIst.Text = "" Or Me.txt2nd.Text = "" Or Me.txt3rd.Text = "" Then
            RadMessageBox.Show("Select Atleast 3 Buckets!")
            Exit Sub
        ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text = "" And Me.txt5th.Text = "" And Me.txt6th.Text = "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
            strNo = 1
            txtOvr = Me.txt3rd.Text
        ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text = "" And Me.txt6th.Text = "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
            strNo = 2
            txtOvr = Me.txt4th.Text
        ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text = "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
            strNo = 3
            txtOvr = Me.txt5th.Text
        ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text <> "" And Me.txt7th.Text = "" And Me.txt8th.Text = "" Then
            strNo = 4
            txtOvr = Me.txt6th.Text
        ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text <> "" And Me.txt7th.Text <> "" And Me.txt8th.Text = "" Then
            strNo = 5
            txtOvr = Me.txt7th.Text
        ElseIf Me.txtIst.Text <> "" And Me.txt2nd.Text <> "" And Me.txt3rd.Text <> "" And Me.txt4th.Text <> "" And Me.txt5th.Text <> "" And Me.txt6th.Text <> "" And Me.txt7th.Text <> "" And Me.txt8th.Text <> "" Then
            strNo = 6
            txtOvr = Me.txtOver.Text
        Else
            RadMessageBox.Show("Selection Criteria Not In Order")
            Exit Sub
        End If

        Dim IsVendorGroupWise As String
        If chkVendorGroupWise.Checked Then
            IsVendorGroupWise = " 1 as IsVendorGroupWise "
        Else
            IsVendorGroupWise = " 0 as IsVendorGroupWise"
        End If

        Dim Qry As String = "select " + IsVendorGroupWise + ",  '" + RunDate + "' as RunDate, '" + AgeOfDate + "' as AgeOfDate, '" + cutoffdate + "' as CutOfDate,  '" + lblcurrent + "' as lblCurent, '" + lbl_1 + "' as lbl_1To15, '" + lbl_2 + "' as lbl_16To30 ,'" + lbl_3 + "' as lbl_31To45, '" + lbl_4 + "' as lbl_46To60 , '" + lbl_5 + "' as lbl_61To75, '" + lbl_6 + "' as lbl_76To90, '" + lbl_7 + "' as lbl_91To105, '" + lbl_8 + "' as lbl_106To120, '" + lbl_Over120 + "' as lbl_Over120, datedifference,  "
        Qry += " 0 as [Current]  , "
        If clsCommon.myLen(txtIst.Text) > 0 Then
            Qry += " case when " + strDtD_1 + " Then (Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 when  Document_Type ='PY' then xxx.Payment_Amount Else [Document_Total] end) Else 0 End as onetofifteen, "
        End If
        If clsCommon.myLen(txt2nd.Text) > 0 Then
            Qry += " case when " + strDtD_2 + " Then (Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 when  Document_Type ='PY' then xxx.Payment_Amount Else [Document_Total] end) Else 0 End as fifteentothirty, "
        End If
        If clsCommon.myLen(txt3rd.Text) > 0 Then
            Qry += " case when " + strDtD_3 + " Then (Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 when  Document_Type ='PY' then xxx.Payment_Amount Else [Document_Total] end) Else 0 End as thirtytofoutyfive , "
        End If
        If clsCommon.myLen(txt4th.Text) > 0 Then
            Qry += " case when " + strDtD_4 + " Then (Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 when  Document_Type ='PY' then xxx.Payment_Amount Else [Document_Total] end) Else 0 End as foutyfivetosixty , "
        Else
            Qry += " 0 as foutyfivetosixty , "
        End If
        If clsCommon.myLen(txt5th.Text) > 0 Then
            Qry += " case when " + strDtD_5 + " Then (Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 when  Document_Type ='PY' then xxx.Payment_Amount Else [Document_Total] end) Else 0 End as sixtytoseventyfive , "
        Else
            Qry += " 0 as sixtytoseventyfive , "
        End If
        If clsCommon.myLen(txt6th.Text) > 0 Then
            Qry += " case when " + strDtD_6 + " Then (Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 when  Document_Type ='PY' then xxx.Payment_Amount Else [Document_Total] end) Else 0 End as seventyfivetoninghty , "
        Else
            Qry += " 0 as seventyfivetoninghty , "
        End If
        If clsCommon.myLen(txt7th.Text) > 0 Then
            Qry += " case when " + strDtD_7 + " Then (Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 when  Document_Type ='PY' then xxx.Payment_Amount Else [Document_Total] end) Else 0 End as ninghtytohundredfive , "
        Else
            Qry += " 0 as ninghtytohundredfive , "
        End If
        If clsCommon.myLen(txt8th.Text) > 0 Then
            Qry += " case when " + strDtD_8 + " Then (Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 when  Document_Type ='PY' then xxx.Payment_Amount Else [Document_Total] end) Else 0 End as hundredfivetohundredtwenty , "
        Else
            Qry += " 0 as hundredfivetohundredtwenty , "
        End If
        Qry += " case when " + strDtD_Over + " Then (Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 when  Document_Type ='PY' then xxx.Payment_Amount Else [Document_Total] end) Else 0 End as abovehundredtwenty ,  "
        'Qry += " case when " + strDtD_Over120 + " and Document_Type NOT IN ('OA','PY','D') then [Document_Total]  when " + strDtD_Over120 + "  and Document_Type ='PY' then xxx.Payment_Amount  else 0 end as abovehundredtwenty ,  "
        Qry += " TSPL_VENDOR_MASTER.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc, DocNo as Vendor_Invoice_No,invno,case when Document_Type='I' then 'IN' when Document_Type='PY' then 'PY' when Document_Type='D' then 'DR' when Document_Type='C' then 'CR' when Document_Type='AV' then 'AV' when Document_Type='OA' then 'OA'when Document_Type='PY' then 'PY' when Document_Type='RC' then 'RC' end as [Document_Type], Convert(Varchar,DocDate, 103) as Vendor_Invoice_Date ,Convert(Varchar,Posting_Date, 103) as Posting_Date, Convert(VARCHAR,Due_Date, 103) as Due_Date ,Document_Total as TotalPaybless , '" + txtOvr + "'  as txtOvr, TSPL_COMPANY_MASTER.Comp_Name , TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2,Convert(date,DocDate, 103) FilterDate  from("

        If chkFifo.Checked = True Then
            Qry += " select Vendor_code,Vendor_Name ,Vendor_Invoice_No as DocNo , Document_Type , COnvert(Date,Invoice_Entry_Date, 103) as DocDate ,Convert(Date,Posting_Date, 103) as Posting_Date, Convert(Date,Due_Date, 103) as Due_Date , (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then  TSPL_VENDOR_INVOICE_HEAD .fifo_balance  Else TSPL_VENDOR_INVOICE_HEAD.Document_Total  End) as [Document_Total], DATEDIFF(dd,convert(date,Due_Date,103),'" + asofdate + "') as datedifference, 0 as Payment_Amount , Vendor_Invoice_No  as invno, TSPL_VENDOR_INVOICE_HEAD.Comp_Code, TSPL_VENDOR_INVOICE_HEAD.Loc_Code AS Location  from TSPL_VENDOR_INVOICE_HEAD  "
            Qry += " where fifo_balance >0 and FIFO_KnockOff <>'Y' and  not(Document_Type in ('D','C') and LEN(refdocno)>0 ) AND ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<> '' And (Len(ISNULL(Against_POInvoice_No, ''))<=0 AND Len(ISNULL(Against_PurchaseReturn_No, ''))<=0) or Document_Type='I'  and TSPL_VENDOR_INVOICE_HEAD.Posting_Date <> '' "
            Qry += " UNION ALL "
            Qry += " select  Vendor_code,Vendor_Name ,TSPL_PAYMENT_HEADER.Payment_No as DocNo ,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type , Convert(Date,Payment_Date, 103) as DocDate , Convert(Date,Payment_Post_Date, 103) as Posting_Date, Convert(Date,Payment_Date, 103) as Due_Date , (( case when  TSPL_PAYMENT_HEADER.Payment_Type='OA' then FIFO_Balance else  Balance_Amt end)- isnull((case when TSPL_PAYMENT_HEADER.Payment_Type in('AV','OA','RC') then (select Amount+ISNULL(TSPL_PAYMENT_HEADER.TDS_Amount,0) from TSPL_BANK_REVERSE where Source_Type='AP' and Document_No=TSPL_PAYMENT_HEADER.Payment_No) else 0 end ),0)) as [Document_Total],DATEDIFF(dd,convert(date,Payment_Date,103), '" + asofdate + "') as datedifference, Case When TSPL_PAYMENT_HEADER.Payment_Type='RC' Then Payment_Amount*-1 Else Payment_Amount End as Payment_Amount,''  as invno, TSPL_PAYMENT_HEADER.Comp_Code, RIGHT(TSPL_BANK_MASTER.BANKACC , 3) as Location  from TSPL_PAYMENT_HEADER  "
            Qry += " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code= TSPL_BANK_MASTER.BANK_CODE where TSPL_PAYMENT_HEADER.Payment_Type <>'PY' and  2=  (case when TSPL_PAYMENT_HEADER.Payment_Type<>'OA' then 2 else  case when TSPL_PAYMENT_HEADER.Payment_Type='OA' and FIFO_Balance >0 then 2 else 0 end end) And (Posted='P' or Posted='1') "
            Qry += " UNION ALL "
            Qry += " select  VC_Code as Vendor_code,VC_Name as Vendor_Name, Document_No as DocNo, case when Amount_Type='Cr' then 'D' else  'C' end as Document_Type,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate,  "
            Qry += " CONVERT(Date,TSPL_VCGL_Head.Posting_Date ,103) as Posting_Date, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as Due_Date, Amount as [Document_Total], DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103) , '" + asofdate + "') as datedifference, amount as Payment_Amount,'' as invno, TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location  from TSPL_VCGL_Head where Document_Type='v' and TSPL_VCGL_Head.Status='1' "
            Qry += " UNION ALL "
            Qry += " select  TSPL_VCGL_Detail.VCGL_Code  as Vendor_code, TSPL_VCGL_Detail.VCGL_Name  as Vendor_Name, TSPL_VCGL_Detail.Document_No as DocNo, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then 'C' when TSPL_VCGL_Detail.Cr_Amount=0.0 then 'D' end as Document_Type, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, CONVERT(Date,TSPL_VCGL_Head.Posting_Date ,103) as Posting_Date, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as Due_Date, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then TSPL_VCGL_Detail.Cr_Amount when TSPL_VCGL_Detail.Cr_Amount=0.0 then TSPL_VCGL_Detail.Dr_Amount end as [Document_Total] ,DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103), '" + asofdate + "') as datedifference, amount as Payment_Amount,'' as invno , TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location from  TSPL_VCGL_Detail  "
            Qry += " left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No   "
            Qry += " where Document_Type='v' AND Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1'"
        Else
            Qry += " select Vendor_code,Vendor_Name ,Vendor_Invoice_No as DocNo , Document_Type , COnvert(Date,Invoice_Entry_Date, 103) as DocDate ,Convert(Date,Posting_Date, 103) as Posting_Date, Convert(Date,Due_Date, 103) as Due_Date , Case When TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 Then TSPL_VENDOR_INVOICE_HEAD.Document_Total Else TSPL_VENDOR_INVOICE_HEAD.Document_Total-TDS_Actual_Amount-ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No Where TSPL_PAYMENT_HEADER.Posted=1 AND TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No AND  TSPL_PAYMENT_HEADER.Payment_Date<=CONVERT(DATE,'" + cutoffdate + "',103)),0)-ISNULL((Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD as VH Where VH.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AND VH.Vendor_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No AND VH.Document_Type='D' AND ISNULL(VH.Against_PurchaseReturn_No,'')<>''),0) End as [Document_Total], DATEDIFF(dd,convert(date,Invoice_Entry_Date,103),'" + asofdate + "') as datedifference, 0 as Payment_Amount , Vendor_Invoice_No  as invno, TSPL_VENDOR_INVOICE_HEAD.Comp_Code, TSPL_VENDOR_INVOICE_HEAD.Loc_Code AS Location  from TSPL_VENDOR_INVOICE_HEAD "
            Qry += " where  Balance_Amt >0 AND ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<> '' And (Len(ISNULL(Against_POInvoice_No, ''))<=0 AND Len(ISNULL(Against_PurchaseReturn_No, ''))<=0) or Document_Type='I'  and TSPL_VENDOR_INVOICE_HEAD.Posting_Date <> ''   "
            Qry += " UNION ALL "
            Qry += " select  Vendor_code,Vendor_Name ,TSPL_PAYMENT_HEADER.Payment_No as DocNo ,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type , Convert(Date,Payment_Date, 103) as DocDate , Convert(Date,Payment_Post_Date, 103) as Posting_Date, Convert(Date,Payment_Date, 103) as Due_Date ,  Balance_Amt+TDS_Amount-isnull((case when TSPL_PAYMENT_HEADER.Payment_Type in('AV','OA','RC') then (select Amount from TSPL_BANK_REVERSE where Source_Type='AP' and Document_No=TSPL_PAYMENT_HEADER.Payment_No AND TSPL_BANK_REVERSE.Post<>'N') else 0 end ),0) as [Document_Total]"

            Qry += " ,DATEDIFF(dd,convert(date,Payment_Date,103), '" + asofdate + "') as datedifference, Case When TSPL_PAYMENT_HEADER.Payment_Type='RC' Then Payment_Amount*-1 Else Payment_Amount End as Payment_Amount,''  as invno, TSPL_PAYMENT_HEADER.Comp_Code, RIGHT(TSPL_BANK_MASTER.BANKACC , 3) as Location  from TSPL_PAYMENT_HEADER "
            Qry += " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code= TSPL_BANK_MASTER.BANK_CODE where TSPL_PAYMENT_HEADER.Payment_Type <>'PY' And (Posted='P' or Posted='1')  "
            Qry += " UNION ALL "
            Qry += " select  VC_Code as Vendor_code,VC_Name as Vendor_Name, Document_No as DocNo, case when Amount_Type='Cr' then 'D' else  'C' end as Document_Type,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, "
            Qry += " CONVERT(Date,TSPL_VCGL_Head.Posting_Date ,103) as Posting_Date, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as Due_Date, Amount as [Document_Total], DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103) , '" + asofdate + "') as datedifference, amount as Payment_Amount,'' as invno, TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location  from TSPL_VCGL_Head  where Document_Type='v' and TSPL_VCGL_Head.Status='1'  "
            Qry += " UNION ALL "
            Qry += " select  TSPL_VCGL_Detail.VCGL_Code  as Vendor_code, TSPL_VCGL_Detail.VCGL_Name  as Vendor_Name, TSPL_VCGL_Detail.Document_No as DocNo, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then 'C' when TSPL_VCGL_Detail.Cr_Amount=0.0 then 'D' end as Document_Type, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, CONVERT(Date,TSPL_VCGL_Head.Posting_Date ,103) as Posting_Date, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as Due_Date, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then TSPL_VCGL_Detail.Cr_Amount when TSPL_VCGL_Detail.Cr_Amount=0.0 then TSPL_VCGL_Detail.Dr_Amount end as [Document_Total] ,DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103), '" + asofdate + "') as datedifference, amount as Payment_Amount,'' as invno , TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location from  TSPL_VCGL_Detail "
            Qry += " left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No   "
            Qry += " where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1'  "
        End If


        Qry += " ) xxx Left Outer Join TSPL_COMPANY_MASTER ON xxx.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN TSPL_VENDOR_MASTER ON xxx.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code where 2=2  and Document_Type  in (" + clsCommon.GetMulcallString(Arr) + " )  and Convert(Date, DocDate, 103) <=Convert(Date,'" + cutoffdate + "', 103) "

        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 1 Then
            Qry += " AND Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If
        If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count >= 1 Then
            Qry += " AND xxx.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
        End If

        If chkFifo.Checked And chkCreditBalance.Checked = True Then
            Qry = "Select * from (" + Qry + ")  ZZZ Where (ISNULL([Current] , 0)+ISNULL(onetofifteen, 0)+ISNULL(fifteentothirty , 0)+ISNULL(thirtytofoutyfive, 0)" & _
    " +ISNULL(foutyfivetosixty, 0) + ISNULL(sixtytoseventyfive, 0) + ISNULL(seventyfivetoninghty, 0) + ISNULL(ninghtytohundredfive, 0) " & _
    " +ISNULL(hundredfivetohundredtwenty, 0)+ISNULL(abovehundredtwenty, 0)) >=0 "
        End If
        If Not chkType.Checked Then
            Qry += " Order by Vendor_Code, FilterDate"
        End If
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Dim frmCRV As New frmCrystalReportViewer()
            If chkType.Checked <> True Then
                frmCRV.funreport(CrystalReportFolder.Purchase, dt, "AgingPaybles" + strNo + "", "A/P Aged Paybles Report")
            Else
                frmCRV.funreport(CrystalReportFolder.Purchase, dt, "AgingPayblesSummaryNew" + strNo + "", "A/P Aged Paybles Report")
            End If
            frmCRV = Nothing
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try

    End Sub




    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged, chkVendorSelect.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub


    Private Sub txt3rd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt3rd.TextChanged

    End Sub

    Private Sub txt3rd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt3rd.Leave
        'If txt3rd.Text = "" Then
        '    txt3rd.Text = 0

        'End If
        txtOver.Text = txt3rd.Text
    End Sub

    Private Sub txtCurrent_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrent.KeyPress
        If Microsoft.VisualBasic.Asc(e.KeyChar) >= 48 And Microsoft.VisualBasic.Asc(e.KeyChar) <= 57 Then
            e.Handled = False
        ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else

            e.Handled = True
        End If
    End Sub

    Private Sub txtIst_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIst.KeyPress
        If Microsoft.VisualBasic.Asc(e.KeyChar) >= 48 And Microsoft.VisualBasic.Asc(e.KeyChar) <= 57 Then
            e.Handled = False
        ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else

            e.Handled = True
        End If
    End Sub

    Private Sub txt2nd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt2nd.KeyPress
        If Microsoft.VisualBasic.Asc(e.KeyChar) >= 48 And Microsoft.VisualBasic.Asc(e.KeyChar) <= 57 Then
            e.Handled = False
        ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else

            e.Handled = True
        End If
    End Sub

    Private Sub txt3rd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt3rd.KeyPress
        If Microsoft.VisualBasic.Asc(e.KeyChar) >= 48 And Microsoft.VisualBasic.Asc(e.KeyChar) <= 57 Then
            e.Handled = False
        ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else

            e.Handled = True
        End If
    End Sub


    Private Sub txtCurrent_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCurrent.Leave
        If txtCurrent.Text = "" Then
            txtCurrent.Text = 0

        End If

    End Sub

    Private Sub txtIst_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIst.Leave
        'If txtIst.Text = "" Then
        '    txtIst.Text = 0
        'End If
    End Sub

    Private Sub txt2nd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt2nd.Leave
        'If txt2nd.Text = "" Then
        '    txt2nd.Text = 0
        'End If
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "AP-AGE-RPT"
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


    Private Sub txt2nd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt2nd.TextChanged

    End Sub

    Private Sub txt8th_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt8th.TextChanged
        txtOver.Text = txt8th.Text
    End Sub


    Private Sub FrmRptAgedPaybles_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'If e.Alt AndAlso e.KeyCode = Keys.N AndAlso  Then
        '    'resetForm()
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            'Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub chkFifo_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkFifo.ToggleStateChanged
        If chkFifo.Checked = True Then
            chkCreditBalance.Visible = True
        Else
            chkCreditBalance.Visible = False
        End If

    End Sub
    Sub loadlocation()
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged, chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
End Class
