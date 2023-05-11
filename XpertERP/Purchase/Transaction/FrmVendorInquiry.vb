'-----10/10/2013-Version(2.0.2.32)--Updation by Pankaj kumar
Imports common
Imports System.Data.SqlClient
Imports XpertERPTDS



Public Class FrmVendorInquiry
    Inherits FrmMainTranScreen
    Const colinvoiceNo As String = "colinvoice"
    Const colInvoiceDate As String = "COLInDate"
    Const colInAmt As String = "COLAMOUNT"
    Const colBalAmt As String = "colBALAMT"
    Const colStatus As String = "COLSTATUS"
    Const colDocNo As String = "COLDOC"


    Const colG2DocType As String = "COLDoc"
    Const colG2DocNo As String = "colDOCNO"
    Const colG2date As String = "COLGDATE"
    Const colG2Amt As String = "COLGAMT"
    Const colG2status As String = "COLSTATUS"




    Private Sub RadSplitContainer3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub gv2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.Click

    End Sub
    Sub LoadBlankGridGV1()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim DocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DocNo.FormatString = ""
        DocNo.HeaderText = "Document No."
        DocNo.Name = colDocNo
        DocNo.Width = 150
        DocNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DocNo)

        Dim InvoiceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        InvoiceNo.FormatString = ""
        InvoiceNo.HeaderText = "Invoice No."
        InvoiceNo.Name = colinvoiceNo
        'InvoiceNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'InvoiceNo.TextImageRelation = TextImageRelation.TextBeforeImage
        InvoiceNo.Width = 150
        InvoiceNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(InvoiceNo)

        Dim InDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        InDate.FormatString = ""
        InDate.HeaderText = "Invoice Date"
        InDate.Name = colInvoiceDate
        InDate.Width = 150
        InDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(InDate)

        Dim Status As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Status.FormatString = ""
        Status.HeaderText = "Status"
        Status.Name = colStatus
        Status.Width = 100
        Status.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Status.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Status)


        Dim InAMt As GridViewDecimalColumn = New GridViewDecimalColumn()
        InAMt.FormatString = ""
        InAMt.HeaderText = "Invoice Amount"
        InAMt.Name = colInAmt
        InAMt.Width = 200
        InAMt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        InAMt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(InAMt)



        Dim BalAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        BalAmt.FormatString = ""
        BalAmt.HeaderText = "Balance Amount"
        BalAmt.Name = colBalAmt
        BalAmt.Width = 200
        BalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        BalAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(BalAmt)




        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub


    Sub LoadBlankGridGV2()
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim G2DocType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        G2DocType.FormatString = ""
        G2DocType.HeaderText = "Doc Type."
        G2DocType.Name = colG2DocType
        G2DocType.Width = 150
        G2DocType.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(G2DocType)


        Dim G2DocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        G2DocNo.FormatString = ""
        G2DocNo.HeaderText = "Doc No."
        G2DocNo.Name = colG2DocNo
        G2DocNo.Width = 150
        G2DocNo.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(G2DocNo)




        Dim G2DocDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        G2DocDate.FormatString = ""
        G2DocDate.HeaderText = "Doc Date"
        G2DocDate.Name = colG2date
        G2DocDate.Width = 150
        G2DocDate.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(G2DocDate)


        Dim GV2Status As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        GV2Status.FormatString = ""
        GV2Status.HeaderText = "Status"
        GV2Status.Name = colG2status
        GV2Status.Width = 100
        GV2Status.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GV2Status.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(GV2Status)



        Dim G2Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        G2Amt.FormatString = ""
        G2Amt.HeaderText = "Amount"
        G2Amt.Name = colG2Amt
        G2Amt.Width = 150
        G2Amt.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(G2Amt)





        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = True
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        gv2.TableElement.TableHeaderHeight = 40
    End Sub
    '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
    Private Sub TxtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtVendorNo._MYValidating
        Try
            Dim Qry As String = "select Vendor_Code AS [Code], Vendor_Name as [Name],ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],(Add1+(case when Add2='' then '' else ',' end)+Add2) as [Address] from TSPL_VENDOR_MASTER"
            Dim WhrCls = " TSPL_VENDOR_MASTER.Status='N' "
            TxtVendorNo.Value = clsCommon.ShowSelectForm("VendSelectfnd", Qry, "Code", WhrCls, TxtVendorNo.Value, "Code", isButtonClicked)
            lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + TxtVendorNo.Value + "'"))
            LoadBlankGridGV1()
            LoadBlankGridGV2()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmVendorInquiry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub FrmVendorInquiry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        reset()

    End Sub

    Sub reset()
        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        LoadBlankGridGV1()
        LoadBlankGridGV2()
        TxtVendorNo.Value = ""
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub fillgridGV1()
        Try

            gv1.DataSource = Nothing
            gv1.Rows.Clear()

            gv2.DataSource = Nothing
            gv2.Rows.Clear()


            Dim strqry As String = "select Document_No, Vendor_Invoice_No,Vendor_Invoice_Date,Document_Total,Balance_Amt,case when Posting_Date IS null then 'Unposted' else 'Posted' end as status from TSPL_VENDOR_INVOICE_HEAD  where Vendor_Code='" + TxtVendorNo.Value + "' and convert(date,Vendor_Invoice_Date,103)>='" + clsCommon.GetPrintDate(dtpFromdate.Value, "yyyy-MM-dd") + "' and convert(date,Vendor_Invoice_Date,103)<='" + clsCommon.GetPrintDate(dtptodate.Value, "yyyy-MM-dd") + "' and Document_Type='I'"
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(strqry)


            If dt.Rows.Count > 0 Then


                For i As Integer = 0 To dt.Rows.Count - 1
                    gv1.Rows.AddNew()
                    gv1.Rows(i).Cells(colDocNo).Value = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    gv1.Rows(i).Cells(colinvoiceNo).Value = dt.Rows(i)("Vendor_Invoice_No").ToString()
                    gv1.Rows(i).Cells(colInvoiceDate).Value = dt.Rows(i)("Vendor_Invoice_Date").ToString()
                    gv1.Rows(i).Cells(colStatus).Value = dt.Rows(i)("status").ToString()
                    gv1.Rows(i).Cells(colInAmt).Value = dt.Rows(i)("Document_Total").ToString()
                    gv1.Rows(i).Cells(colBalAmt).Value = dt.Rows(i)("Balance_Amt").ToString()
                Next
                gridformatgv1()
            Else
                common.clsCommon.MyMessageBoxShow("No Data found")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            fillgridGV1()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try

            Dim docno As String
            docno = gv1.CurrentRow.Cells(colDocNo).Value.ToString()
            fillGV2(docno)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Sub fillGV2(ByVal docno As String)
        Try

            Dim dn As String = docno
            gv2.DataSource = Nothing
            gv2.Rows.Clear()

            '-------------BM00000003394
            Dim qry As String = "select  'Payment' as DocType,TSPL_PAYMENT_HEADER.Payment_No as DocNo, CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103) as DocDate,TSPL_PAYMENT_DETAIL.Applied_Amount as DocAmt, case when (TSPL_PAYMENT_HEADER.Posted='1' or TSPL_PAYMENT_HEADER.Posted='P') then 'Posted' else 'Unposted' end as Status from TSPL_PAYMENT_HEADER left outer join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No where TSPL_PAYMENT_DETAIL.Document_No='" + dn + "' " & _
            " union all " & _
            " select 'Purchase Return' as DocType,PR_No as DocNo,convert(VARCHAR, PR_Date, 103) as DocDate,PR_Total_Amt as DocAmt,case when Status='1' then 'Posted' else 'Unposted' end as Status from TSPL_PR_HEAD LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PR_HEAD.PR_No  where TSPL_VENDOR_INVOICE_HEAD.Document_No = '" + dn + "' " & _
               " union all " & _
            " select 'Purchase Return' as DocType,PR_No as DocNo,convert(VARCHAR, PR_Date, 103) as DocDate,PR_Total_Amt as DocAmt,case when TSPL_PR_HEAD.Status='1' then 'Posted' else 'Unposted' end as Status from TSPL_PR_HEAD left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PR_HEAD.Against_PI LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No  where TSPL_VENDOR_INVOICE_HEAD.Document_No = '" + dn + "' " & _
              " union all " & _
           "  select   'TDS' as DocType, Remittance_Code as DocNo, CONVERT(VARCHAR,Document_Date,103) as DocDate,Actual_Total_TDS as DocAmt,  case when Remit_TDS='Y' then 'Posted' else 'Unposted' end as Status  from TSPL_REMITTANCE where Document_No='" + dn + "' "

            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)


            If dt.Rows.Count > 0 Then



                For i As Integer = 0 To dt.Rows.Count - 1
                    gv2.Rows.AddNew()
                    'gv2.Rows(i).Cells(colG2DocType).Value = dt.Rows(i)(0).ToString()
                    'gv2.Rows(i).Cells(colG2DocNo).Value = dt.Rows(i)(1).ToString()
                    'gv2.Rows(i).Cells(colG2date).Value = dt.Rows(i)(2).ToString()
                    'gv2.Rows(i).Cells(colG2status).Value = dt.Rows(i)(3).ToString()
                    'gv2.Rows(i).Cells(colG2Amt).Value = dt.Rows(i)(4).ToString()

                    gv2.Rows(i).Cells(colG2DocType).Value = dt.Rows(i)("DocType").ToString()
                    gv2.Rows(i).Cells(colG2DocNo).Value = dt.Rows(i)("DocNo").ToString()
                    gv2.Rows(i).Cells(colG2date).Value = dt.Rows(i)("DocDate").ToString()
                    gv2.Rows(i).Cells(colG2status).Value = dt.Rows(i)("Status").ToString()
                    gv2.Rows(i).Cells(colG2Amt).Value = dt.Rows(i)("DocAmt").ToString()
                Next
                gridformat()
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Public Sub gridformat()
        Try

            gv2.MasterTemplate.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim SumContainerDp As New GridViewSummaryItem(colG2Amt, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumContainerDp)
            gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Public Sub gridformatgv1()
        Try

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim SumContainerDp As New GridViewSummaryItem(colInAmt, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumContainerDp)
            Dim SumContainerBal As New GridViewSummaryItem(colBalAmt, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumContainerBal)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub


    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub gv2_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub gv2_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellDoubleClick
        Dim DocNo As String
        Dim DocType As String
        DocNo = clsCommon.myCstr(gv2.CurrentRow.Cells(colG2DocNo).Value)
        DocType = clsCommon.myCstr(gv2.CurrentRow.Cells(colG2DocType).Value)
        If clsCommon.CompairString(DocType, "Payment") = CompairStringResult.Equal Or clsCommon.CompairString(DocType, "AP-MI") = CompairStringResult.Equal Then
            Dim frm As New FrmPaymentNew
            frm.SetUserMgmt(clsUserMgtCode.PaymentEntryNew)
            frm.Show()
            frm.LoadData(DocNo, NavigatorType.Current)
        ElseIf clsCommon.CompairString(DocType, "Purchase Return") = CompairStringResult.Equal Then
            Dim frm As New frmPurchaseReturn
            frm.SetUserMgmt(clsUserMgtCode.mbtnPurchaseReturn)
            frm.Show()
            frm.LoadData(DocNo, NavigatorType.Current)
        ElseIf clsCommon.CompairString(DocType, "TDS") = CompairStringResult.Equal Then
            Dim frm As New Frmremittanceentry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            frm.SetUserMgmt(clsUserMgtCode.remittanceentry)
            frm.Show()
            frm.fndremittance.Value = DocNo
            frm.RemittanceChanged()
        End If
    End Sub
End Class
